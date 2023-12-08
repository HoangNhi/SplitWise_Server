using BE_WiseWallet.Enums;
using System.ComponentModel.DataAnnotations;

namespace BE_WiseWallet.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PaidById { get; set; }
        public virtual Image Image { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public SplitType SplitType { get; set; }
        public Boolean IsRemoved { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
