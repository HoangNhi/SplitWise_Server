using BE_WiseWallet.Enums;
using System.ComponentModel.DataAnnotations;

namespace BE_WiseWallet.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int SplitId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public PaymentMethod Payment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
