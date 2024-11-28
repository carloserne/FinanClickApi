using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinanClickApi.Models
{
    public class PaymentRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } // Amount in USD
        public string? StripePaymentIntentId { get; set; }
        public string Status { get; set; } // Pending, Paid, Failed
        public int IdEmpresa { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; } // Fecha de pago

        [JsonIgnore]
        public virtual Empresa? IdEmpresaNavigation { get; set; } = null!;
    }

}
