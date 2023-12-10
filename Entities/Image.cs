using System.ComponentModel.DataAnnotations;

namespace BE_WiseWallet.Entities
{
    public class Image
    {

        [Key]
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
    }
}
