using Microsoft.AspNetCore.Mvc;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System.Security.Claims;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartService;
        public CartController(ICartRepository _cartService)
        {
            this._cartService = _cartService;
        }
        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
            }

            return View(cart); 
        }
        [HttpPost]
        public async Task<JsonResult> UpdateQuantity(int productId, int change)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.UpdateQuantity(userId, productId, change);

            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> RemoveItem(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Json(new { success = false, message = "User not authorized" });

            try
            {
                var result = await _cartService.RemoveItem(userId, productId);

                if (result!=null)
                {
                    var cart = await _cartService.GetCartByUserIdAsync(userId);
                    int count = cart.Items.Count;

                    // Update the CartCount cookie
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    };
                    HttpContext.Response.Cookies.Append("CartCount", count.ToString(), options);

                    return Json(new { success = true, cartCount = count });
                }

                return Json(new { success = false, message = "Failed to remove item from cart" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId,int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            try
            {
               
                await _cartService.AddToCartAsync(userId, productId,quantity);


                var cart = await _cartService.GetCartByUserIdAsync(userId);

                
                int count = cart.Items.Count;

                // Save the cart count in cookies
                HttpContext.Response.Cookies.Append("CartCount", count.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7), 
                    HttpOnly = true, 
                    Secure = true 
                });

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cartCount = HttpContext.Request.Cookies["CartCount"] ?? "0";
            return Content(cartCount);
        }

    }
}
