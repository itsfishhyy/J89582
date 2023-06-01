using Microsoft.Build.Framework;

namespace J89582.Data
{
    public class OrderItem
    {
        [Required]
        public int OrderNo { get; set; }
        [Required]
        public int StockID { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
