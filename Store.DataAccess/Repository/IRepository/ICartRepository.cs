using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface ICartRepository
    {
        Task AddToCartAsync(string userId, int productId,int quantity);
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task<JsonResult> UpdateQuantity(string userId, int productId, int change);
        Task<JsonResult> RemoveItem(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}
