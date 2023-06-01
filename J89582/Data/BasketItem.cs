using Microsoft.Build.Framework;

namespace J89582.Data
{
    public class BasketItem
    {
        [Required]
        public int StockID { get; set; }
        [Required]
        public int BasketID { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
