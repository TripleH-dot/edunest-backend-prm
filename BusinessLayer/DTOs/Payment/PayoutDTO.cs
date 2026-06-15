using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Payment
{
    public sealed class RequestPayoutRequest
    {
        [Range(1000, double.MaxValue)]
        public decimal Amount { get; set; }
    }

    public sealed class PayoutResponse
    {
        public int PayoutId { get; set; }
        public int TutorId { get; set; }
        public decimal Amount { get; set; }

        // Pending, Paid, Rejected, Failed
        public string Status { get; set; } = string.Empty;

        // ManualQr
        public string PayoutMethod { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        // Tutor bank info for admin manual QR backup
        public string? TutorBankName { get; set; }
        public string? TutorBankBin { get; set; }
        public string? TutorAccountNumber { get; set; }
        public string? TutorAccountHolderName { get; set; }
        public string? TutorBankBranch { get; set; }
    }

    public sealed class AdminUpdatePayoutRequest
    {
        [Required, MaxLength(50)]
        public string Status { get; set; } = "Paid";
    }

}
