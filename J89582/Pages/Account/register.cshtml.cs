using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using J89582.Data;
using Microsoft.EntityFrameworkCore;

namespace J89582.Pages.Account
{
    public class registerModel : PageModel
    {
        [BindProperty]
        public RegistrationModel Input { get; set; }

        private J89582Context _db;
        public CheckoutCust Customer = new CheckoutCust();
        public Basket Basket = new Basket();


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<registerModel> _logger;

        public registerModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<registerModel> logger,
            J89582Context db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _db = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid) 
            {
                Task<bool> hasRegUserRole = _roleManager.RoleExistsAsync("RegisteredUser");
                hasRegUserRole.Wait();

                if (!hasRegUserRole.Result)
                {
                    var roleResult = _roleManager.CreateAsync(new IdentityRole("RegisteredUser"));
                    roleResult.Wait(); 
                }

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    NewBasket();
                    NewCustomer(Input.Email);
                    await _db.SaveChangesAsync();

                    Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(user, "RegisteredUser");
                    newUserRole.Wait();
                    return RedirectToPage("/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
        public void NewBasket()
        {
            var currentBasket = _db.Baskets.FromSqlRaw("SELECT * From Baskets")
                .OrderByDescending(b => b.BasketID)
                .FirstOrDefault();
            if (currentBasket == null)
            {
                Basket.BasketID = 1;
            }
            else
            {
                Basket.BasketID = currentBasket.BasketID + 1;
            }

            _db.Baskets.Add(Basket);
        }

        public void NewCustomer(string Email)
        {
            Customer.Email = Email;
            Customer.BasketID = Basket.BasketID;
            _db.CheckoutCust.Add(Customer);
        }
    }

}

