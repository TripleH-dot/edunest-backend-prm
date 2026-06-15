using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Table("Payouts")]
    public class Payout
    {
        [Key]
        public int PayoutId { get; set; }

        public int TutorId { get; set; }

        public int? WalletTransactionId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedAt { get; set; }

        public DateTime? PaidAt { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } = "Pending";
        // Pending / Paid / Rejected / Failed

        [Required, MaxLength(50)]
        public string PayoutMethod { get; set; } = "ManualQr";
        // ManualQr

        [ForeignKey(nameof(TutorId))]
        public virtual Tutor Tutor { get; set; } = null!;

        [ForeignKey(nameof(WalletTransactionId))]
        public virtual WalletTransaction? WalletTransaction { get; set; }
    }
}
