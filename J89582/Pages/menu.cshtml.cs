using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using J89582.Data;
using J89582.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace J89582.Pages
{
    
    public class MenuModel : PageModel
    {
        
        private readonly J89582.Data.J89582Context _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MenuModel(J89582.Data.J89582Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IList<Menu> Menu { get; set; } = default!;

        

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                List<Menu> menus = await _context.Menu.ToListAsync();
                Menu = menus;
            }
        }

        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            CheckoutCust customer = await _context
            .CheckoutCust
            .FindAsync(user.Email);
            var item = _context.BasketItems.FromSqlRaw("SELECT * FROM BasketItems WHERE StockID = {0}" + " AND BasketID = {1}", itemID, customer.BasketID)
                        .ToList()
                        .FirstOrDefault();

            if (item == null)
            {
                BasketItem newItem = new BasketItem
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };
                _context.BasketItems.Add(newItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                item.Quantity = item.Quantity + 1;
                _context.Attach(item).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket not found!", e);
                }
            }
            return RedirectToPage();
        }

    }
}
