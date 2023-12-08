using System.ComponentModel.DataAnnotations;

namespace BE_WiseWallet.Entities
{
    public class Split
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public int PaidById { get; set; }
        public double Paid { get; set; }
        public double Owed { get; set; }
        public Boolean IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;
    }
}
