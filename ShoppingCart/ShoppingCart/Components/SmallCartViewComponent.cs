using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModel;

namespace ShoppingCart.Components
{
	public class SmallCartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");
			SmallCartViewModel smallCartVM;

			if (cartItems == null || cartItems.Count == 0)
				smallCartVM = null;
			else
			{
				smallCartVM = new()
				{
					NumberOfItems = cartItems.Sum(x => x.Quantity),
					TotalAmount = cartItems.Sum(x => x.Quantity * x.Price)
				};
			}

			return View(smallCartVM);
		}
		
	}
}
