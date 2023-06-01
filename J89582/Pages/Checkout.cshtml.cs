using J89582.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;


namespace J89582.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly J89582Context _db;
        private readonly UserManager<ApplicationUser>? _UserManager;
        public CheckoutModel(UserManager<ApplicationUser> userManager, J89582Context db)
        {
            _UserManager = userManager;
            _db = db;
        }
        public IList<CheckoutItems> Items { get; private set; }
        public OrderHistory Order = new OrderHistory();

        public decimal Total = 0;
        public long AmountPayable = 0;


        public async Task OnGetAsync()
        {
            var user = await _UserManager.GetUserAsync(User);
            CheckoutCust customer = await _db.CheckoutCust.FindAsync(user.Email);

            Items = _db.CheckoutItems.FromSqlRaw(
                "SELECT Menu.ID, Menu.Price, Menu.MenuName," +
                "BasketItems.BasketID, BasketItems.Quantity " +
                "FROM Menu INNER JOIN BasketItems " +
                "ON Menu.ID = BasketItems.StockID " +
                "WHERE BasketID = {0}", customer.BasketID
                ).ToList();

            Total = 0;
            foreach (var item in Items)
            {
                Total += (item.Quantity * item.Price);
            }
            AmountPayable = (long)(Total * 100);

        }

        public async Task<IActionResult> OnPostBuyAsync()
        {
            var currentOrder = _db.OrderHistories
            .FromSqlRaw("SELECT * From OrderHistories")
                .OrderByDescending(b => b.OrderNo)
                .FirstOrDefault();

            if (currentOrder == null)
            {
                Order.OrderNo = 1;
            }
            else
            {
                Order.OrderNo = currentOrder.OrderNo + 1;
            }
            var user = await _UserManager.GetUserAsync(User);
            Order.Email = user.Email;
            _db.OrderHistories.Add(Order);

            CheckoutCust customer = await _db
                .CheckoutCust
                .FindAsync(user.Email);

            var basketItems =
                _db.BasketItems
                .FromSqlRaw("SELECT * From BasketItems " +
                "WHERE BasketID = {0}", customer.BasketID)
                .ToList();

            foreach (var item in basketItems)
            {
                OrderItem oi = new OrderItem
                {
                    OrderNo = Order.OrderNo,
                    StockID = item.StockID,
                    Quantity = item.Quantity
                };
                _db.OrderItems.Add(oi);
                _db.BasketItems.Remove(item);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");

        }

        public IActionResult OnPostCharge(
        string stripeEmail,
        string stripeToken,
        long amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = amount,
                Description = "CO5227 Burgeroorzo Charge",
                Currency = "BND",
                Customer = customer.Id
            });

            return RedirectToPage("/Index");
        }

    }


}
