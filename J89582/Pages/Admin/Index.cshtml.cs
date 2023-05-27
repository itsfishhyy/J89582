using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using J89582.Data;
using J89582.Model;
using Microsoft.AspNetCore.Authorization;

namespace J89582.Pages.Admin
{

    public class IndexModel : PageModel
    {
        private readonly J89582.Data.J89582Context _context;

        public IndexModel(J89582.Data.J89582Context context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Menu != null)
            {
                Menu = await _context.Menu.ToListAsync();
            }
        }
    }
}
