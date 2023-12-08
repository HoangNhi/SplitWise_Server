using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_WiseWallet.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public int LeaderId { get; set; }
        public ICollection<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
        public string Name { get; set; }
        public Image Image { get; set; }
        public string LinkInvite { get; set; }
        
        // This property use for join team by sign Code
        public string CodeInvite { get; set; }
        public double TotalPaid { get; set; } = 0;
        public Boolean isCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }  
}
