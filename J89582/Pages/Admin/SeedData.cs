using Microsoft.EntityFrameworkCore;
using J89582.Data;
using J89582.Model;

namespace J89582.Pages.Admin
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new J89582Context(serviceProvider.GetRequiredService<
                DbContextOptions<J89582Context>>))
            {
                if(context == null || context.Menu == null)
                {
                    throw new ArgumentException("Null J89582Context");
                }

                if (context.Menu.Any())
                {
                    return;
                }

                context.Menu.AddRange(
                    new Menu
                    {
                        Name = "Double Cheese Burger",
                        Price = 3.50
                    },

                    new Menu
                    {
                        Name = "Barbeque Chicken Burger",
                        Price = 4.50
                    }

                    );
                context.SaveChanges();
            }
        }
    }
}
