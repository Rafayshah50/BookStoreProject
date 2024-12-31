using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicatonDbContext _context;

        public OrderRepository(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task<UserDetails> GetUserDetailsByIdAsync(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDetails
                {
                    UserName = u.UserName,
                    Address = u.StreetAddress,
                    Phone = u.PhoneNumber,
                    PostalCode=u.PostalCode,
                    City = u.City,
                    State=u.State
                })
                .FirstOrDefaultAsync();
        }
        public async Task<Order> CreateOrder(string userId, Cart cart, string fullName, string phoneNumber, string address, string city, string state, string postalCode)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Items.Sum(item => item.Price * item.Quantity),
                PaymentStatus = "Paid",
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                State = state,
                PostalCode = postalCode,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in cart.Items)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Title,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity,
                });
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<IEnumerable<Order>> GetOrderByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(c => c.OrderDetails)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
        public async Task<Order> GetOrderDetail(int Id)
        {
            return await _context.Orders
            .Include(o => o.OrderDetails) 
            .FirstOrDefaultAsync(o => o.Id == Id);
        }

    }
}
