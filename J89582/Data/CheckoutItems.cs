using System.ComponentModel.DataAnnotations;

namespace J89582.Data
{
    public class CheckoutItems
    {
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(50)]
        public string? MenuName { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
