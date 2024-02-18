using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.CartDataService
{
    public interface ICartData
    {
        public List<Cart> FetchCartProducts(string userId);

        public Task<bool> ItemPresentInCart(string userId, Guid productId);

        public Task AddCartItem(Cart cart);

        public Task RemoveCartItem(string userId, Guid productId);

        public Task UpdateQuantity(string userId, Guid productId, int quantity);
    }
}
