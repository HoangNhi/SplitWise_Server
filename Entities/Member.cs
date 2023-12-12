using BE_WiseWallet.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_WiseWallet.Entities
{
    public class Member
    {
        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public MemberStatus Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

    }
}
