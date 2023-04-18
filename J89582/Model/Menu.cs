using System.ComponentModel.DataAnnotations;
namespace J89582.Model
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
