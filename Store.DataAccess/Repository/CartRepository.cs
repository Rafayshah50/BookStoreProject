using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicatonDbContext _context;

        public CartRepository(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, int productId,int quantity)
        {
            
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception("Product not found");

            
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Cart.Add(cart);
            }

           
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = (decimal)product.Price
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Cart
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<JsonResult> UpdateQuantity(string userId, int productId, int change)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new JsonResult(new { success = false, message = "Cart not found" });
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
            {
                return new JsonResult(new { success = false, message = "Product not found in the cart" });
            }

            cartItem.Quantity += change;

            if (cartItem.Quantity <= 0)
            {
                cart.Items.Remove(cartItem);
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
        public async Task<JsonResult> RemoveItem(string userId, int productId)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new JsonResult(new { success = false, message = "Cart not found" });
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
            {
                return new JsonResult(new { success = false, message = "Product not found in the cart" });
            }
            cart.Items.Remove(cartItem);

            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }
        public async Task ClearCartAsync(string userId)
        {
            var cart = await _context.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }


    }

}
