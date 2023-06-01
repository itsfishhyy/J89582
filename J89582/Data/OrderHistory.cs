
using System.ComponentModel.DataAnnotations;

namespace J89582.Data
{
    public class OrderHistory
    {
        [Key, Required]
        public int OrderNo { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
