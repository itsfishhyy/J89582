using System.ComponentModel.DataAnnotations;

namespace J89582.Data
{
    public class CheckoutCust
    {
        [Key]
        [StringLength(50)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public int BasketID { get; set; }

    }
}
