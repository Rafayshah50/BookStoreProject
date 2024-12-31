using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IOrderRepository 
    {
        Task<UserDetails> GetUserDetailsByIdAsync(string userId);
        Task<Order> CreateOrder(string userId, Cart cart, string fullName, string phoneNumber, string address, string city, string state, string postalCode);
        Task<IEnumerable<Order>> GetOrderByUserIdAsync(string userId);
        Task<Order> GetOrderDetail(int userId);
    }
}
