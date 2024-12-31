using Store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IOrderManagement
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, string newStatus);
    }
}

