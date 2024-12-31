using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using Newtonsoft.Json;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PaymentController : Controller
    {
        private readonly ICartRepository _cartService;
        private readonly IOrderRepository _orderRepository;

        public PaymentController(ICartRepository cartService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InitiateStripe(string userName, string phone, string address, string city, string state, string postal)
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { error = "User not logged in" });
            }

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                return Json(new { error = "Cart is empty" });
            }
            var userDetails = new UserDetails
            {
                UserName = userName,
                Phone = phone,
                Address = address,
                City = city,
                State = state,
                PostalCode = postal
            };
            TempData["UserDetails"] = JsonConvert.SerializeObject(userDetails);
            var domain = $"{Request.Scheme}://{Request.Host.Value}";
            var lineItems = new List<SessionLineItemOptions>();

            foreach (var item in cart.Items)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Title,
                        },
                        UnitAmount = (long)(item.Price * 100),
                    },
                    Quantity = item.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{domain}/Customer/Payment/Success?sessionId={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/Customer/Payment/Cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Json(new { sessionId = session.Id, url = session.Url });
        }

        [HttpGet]
        public async Task<IActionResult> Success(string sessionId)
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            if (session == null || session.PaymentStatus != "paid")
            {
                ViewBag.Message = "Payment verification failed.";
                return View("Cancel");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                ViewBag.Message = "Cart is empty.";
                return View("Cancel");
            }
            var userDetailsJson = TempData["UserDetails"] as string;
            var userDetails = JsonConvert.DeserializeObject<UserDetails>(userDetailsJson);
            var order = await _orderRepository.CreateOrder(
                userId,
                cart,
                userDetails.UserName,
                userDetails.Phone,
                userDetails.Address,
                userDetails.City,
                userDetails.State,
                userDetails.PostalCode
            );
            await _cartService.ClearCartAsync(userId);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true
            };
            HttpContext.Response.Cookies.Append("CartCount", "0", options);

            ViewBag.Message = "Payment was successful!";
            return View(order);
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return RedirectToAction("ReviewOrder", "Order");
        }
    }
}
