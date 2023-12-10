using System.ComponentModel.DataAnnotations;

namespace BE_WiseWallet.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string content { get; set; }
        public Boolean isMessage { get; set; } = false;
        public Boolean isRemoved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
