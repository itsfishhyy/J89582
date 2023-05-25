using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace J89582.Model
{
    public class Menu
    {
        public int ID { get; set; }
        
        public string MenuName { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
