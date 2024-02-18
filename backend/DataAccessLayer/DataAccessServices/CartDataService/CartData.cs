using BusinessObjectLayer.Data;
using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.CartDataService
{
    public class CartData: ICartData
    {
        private readonly ManageDb _context;

        public CartData(ManageDb context)
        {
            _context = context;
        }

        public List<Cart> FetchCartProducts(string userId)
        {
            var cartItems = _context.Carts.Where(item => item.Email.Equals(userId)).ToList();
            return cartItems;
        }

        public async Task<bool> ItemPresentInCart(string userId, Guid productId)
        {
            var items = await _context.Carts.FindAsync(userId, productId);
            if(items == null)
            {
                return false;
            }
            return true;
        }

        public async Task AddCartItem(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();  
        }

        public async Task RemoveCartItem(string userId, Guid productId)
        {
            var item = await _context.Carts.FindAsync(userId, productId);

            if(item != null)
            {
                _context.Carts.Remove(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantity(string userId, Guid productId, int quantity)
        {
            var item = await _context.Carts.FindAsync(userId, productId);

            if(item != null)
            {
                item.Quantity = quantity;
            }

            await _context.SaveChangesAsync();
        }
    }
}
