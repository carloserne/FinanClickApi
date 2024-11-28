using Stripe;
using Stripe.Checkout;

namespace FinanClickApi.Services
{
    public class StripeService
    {
        public async Task<(string Url, string PaymentIntentId)> CreateCheckoutSession(decimal amount, string successUrl, string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "mxn",
                    UnitAmount = (long)(amount * 100), // Convertir a centavos
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Payment for Service" // Nombre del producto
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            // Devolver tanto la URL de la sesión como el PaymentIntentId asociado
            return (session.Url, session.Id);
        }

        public async Task<string> CreatePaymentIntent(decimal amount)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convert to cents
                Currency = "mxn",
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.Id;
        }
    }

}
