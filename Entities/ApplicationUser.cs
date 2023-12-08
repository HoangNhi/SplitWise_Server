using Microsoft.AspNetCore.Identity;

namespace BE_WiseWallet.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public virtual Image? Image { get; set; }
        public Boolean isRemoved { get; set; } = false;
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
