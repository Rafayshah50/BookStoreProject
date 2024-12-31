using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;

namespace Store.DataAccess.Repository
{
    public class OrderManagement : IOrderManagement
    {
        private readonly ApplicatonDbContext _context;

        public OrderManagement(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            order.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
