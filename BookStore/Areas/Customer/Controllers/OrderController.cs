using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System.Security.Claims;

namespace BookStore.Areas.Customer.Controllers
{   
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderService;
        private readonly ICartRepository _cartService;

        public OrderController(IOrderRepository _orderService, ICartRepository _cartService)
        {
            this._orderService = _orderService;
            this._cartService = _cartService;
        }
        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var orders = await _orderService.GetOrderByUserIdAsync(userId);
            return View(orders);
        }
        public async Task<IActionResult> ReviewOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            var userDetails = await _orderService.GetUserDetailsByIdAsync(userId);
            if (userDetails == null || cart == null)
            {
                return NotFound(); 
            }
            var cartItems = cart.Items;
            var totalPrice = cartItems.Sum(ci => ci.Price * ci.Quantity);
            var VM = new ReviewOrderViewModel
            {
                UserName = userDetails.UserName,
                Address = userDetails.Address,
                City = userDetails.City,
                State = userDetails.State,
                PostalCode = userDetails.PostalCode,
                Phone = userDetails.Phone,
                CartItems = (List<CartItem>)cartItems,
                TotalPrice = totalPrice
            };

            return View(VM);
        }
        public async Task<IActionResult> OrderDetails(int id)
        {
           
            var orders = await _orderService.GetOrderDetail(id);
            var VM = new ViewModelForDetail
            {
                UserName = orders.FullName,
                Address = orders.Address,
                City = orders.City,
                State = orders.State,
                PostalCode = orders.PostalCode,
                Phone = orders.PhoneNumber,
                OrderDetails = (List<OrderDetail>)orders.OrderDetails,
                TotalPrice = orders.TotalAmount
            };
            return View(VM);
        }
    }
}

