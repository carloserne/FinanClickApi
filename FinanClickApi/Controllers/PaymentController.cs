using FinanClickApi.Migrations;
using FinanClickApi.Models;
using FinanClickApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.V2;
using System.Net.NetworkInformation;

namespace FinanClickApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly FinanclickDbContext _context;
        private readonly StripeService _stripeService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;


        public PaymentController(FinanclickDbContext context, StripeService stripeService, EmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _stripeService = stripeService;
            _emailService = emailService;
            _configuration = configuration;
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Verificar la firma del webhook
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _configuration["Stripe:webhookSecret"], // Obtén el Webhook Secret desde appsettings.json
                    throwOnApiVersionMismatch: false

                );

                // Procesar el evento checkout.session.completed
                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    // Busca la solicitud de pago en la base de datos
                    var paymentRequest = _context.PaymentRequests
                        .FirstOrDefault(p => p.StripePaymentIntentId == session.Id);

                    if (paymentRequest != null)
                    {
                        // Actualizar el estado del pago
                        var ingresosBody = new IngresosEgreso
                        {
                            Fecha = DateOnly.FromDateTime(DateTime.UtcNow),
                            TipoTransaccion = 1,
                            Monto = paymentRequest.Amount > 0 ? paymentRequest.Amount : throw new ArgumentException("El monto debe ser mayor que 0."),
                            Descripcion = "Pago por Suscripción Prueba",
                            Categoria = "Ingresos",
                            Estatus = 1,
                            IdEmpresa = paymentRequest.IdEmpresa
                        };


                        _context.IngresosEgresos.Add(ingresosBody);
                        await _context.SaveChangesAsync();

                        paymentRequest.Status = "Paid";
                        paymentRequest.PaidAt = DateTime.UtcNow; // Marca la fecha de pago
                        await _context.SaveChangesAsync();
                    }
                }

                return Ok(); // Stripe requiere un 200 OK como respuesta al webhook
            }
            catch (StripeException e)
            {
                return BadRequest($"Stripe exception: {e.Message}");
            }
        }


        [HttpGet("get-proximo-pago")]
        public async Task<IActionResult> proximos_pagos()
        {

            var ventas_prospecto = await _context.VentaProspectos.Where(v => v.IdIngresoEgreso != null).ToListAsync();

            var pagos_proximos = new List<PaymentRequest>();

            foreach (var v in ventas_prospecto)
            {
                var ingresoEgreso = await _context.IngresosEgresos
                    .Where(i => i.IdIngresosEgresos == v.IdIngresoEgreso)
                    .FirstOrDefaultAsync();

                if (ingresoEgreso == null) continue;

                var fechaIngreso = ingresoEgreso.Fecha;
                var idEmpresa = ingresoEgreso.IdEmpresa;
                var plan = await _context.PlanEmpresas.Where(p => p.IdPlan == v.IdPlan).FirstOrDefaultAsync();
                if (idEmpresa != null)
                {
                    var pagosRealizados = await _context.PaymentRequests
                        .Where(p => p.IdEmpresa == idEmpresa)
                        .ToListAsync();



                    if (!pagosRealizados.Any())
                    {
                        var request = new PaymentRequest
                        {
                            Amount =  (decimal) plan.Precio,
                            IdEmpresa = idEmpresa.Value,
                            StripePaymentIntentId = DateTime.UtcNow.ToString() + idEmpresa.ToString(),
                            Status = "Pending",
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.PaymentRequests.Add(request);
                    }
                    else
                    {
                        if (plan == null) continue;

                        var mesesPlan = int.TryParse(plan.Duracion, out var duracion) ? duracion : 0;
                        var ultimoPago = pagosRealizados.LastOrDefault();

                        if (ultimoPago?.Status == "Paid")
                        {
                            var proximoPagoFecha = ultimoPago.CreatedAt.AddMonths(mesesPlan);

                            if (proximoPagoFecha <= DateTime.UtcNow)
                            {
                                var request = new PaymentRequest
                                {
                                    Amount = (decimal) plan.Precio,
                                    IdEmpresa = idEmpresa.Value,
                                    Status = "Pending",
                                    StripePaymentIntentId = DateTime.UtcNow.ToString(),
                                    CreatedAt = DateTime.UtcNow
                                };
                                _context.PaymentRequests.Add(request);
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            
            
            await SendPaymentsRequests();

            await _context.SaveChangesAsync();


            var pagos = await _context.PaymentRequests.ToListAsync();


            return Ok(pagos);
        }


        [NonAction]
        public async Task SendPaymentsRequests()
        {
            var successUrl = "https://tusitio.com/success";
            var cancelUrl = "https://tusitio.com/cancel";

            // Retrieve all pending payments
            var pagosPendientes = await _context.PaymentRequests.Where(p => p.Status == "Pending").ToListAsync();

            foreach (var request in pagosPendientes)
            {
                // Create Checkout Session
                var (sessionUrl, paymentIntentId) = await _stripeService.CreateCheckoutSession(request.Amount, successUrl, cancelUrl);

                // Update request details
                request.StripePaymentIntentId = paymentIntentId;
                request.Status = "Pending";
                request.PaidAt = DateTime.UtcNow; // Mark the payment date

                // Save changes for the current request
                _context.PaymentRequests.Update(request);

                // Send emails
                var usuarios = await _context.ContactoPersonas
                                             .Where(cp => cp.IdEmpresa == request.IdEmpresa)
                                             .ToListAsync();

                var emails = usuarios
                             .Where(u => !string.IsNullOrEmpty(u.Email))
                             .Select(u => u.Email)
                             .ToList();

                var emailBody = $"Por medio del presente \n Se solicita el pago del monto de {request.Amount}\n{sessionUrl}";

                foreach (var email in emails)
                {
                    await _emailService.SendEmailAsync(email, "Requisito de Pago", emailBody);
                }
            }
        }


    }



}



