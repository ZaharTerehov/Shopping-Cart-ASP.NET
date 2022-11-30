using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModel;

namespace ShoppingCart.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CartController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Quantity * x.Price)
			};

			return View(cartVM);
		}

        public async Task<IActionResult> Add(Guid id)
        {
			Product product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

			if(cartItem == null)
				cart.Add(new CartItem(product));
			else
				cartItem.Quantity += 1;

			HttpContext.Session.SetJson("Cart", cart);

			TempData["Success"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
