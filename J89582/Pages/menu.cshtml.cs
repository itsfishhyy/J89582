using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using J89582.Data;
using J89582.Model;
using Microsoft.EntityFrameworkCore;

namespace J89582.Pages
{
    public class MenuModel : PageModel
    {
        
        private readonly J89582.Data.J89582Context _context;

        public MenuModel(J89582.Data.J89582Context context)
        {
            _context = context;
        }
        public IList<Menu> Menu { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                Menu = await _context.Menu.ToListAsync();
            }
        }
    }
}
