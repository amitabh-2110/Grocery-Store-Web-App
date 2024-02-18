using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.CartLogicService
{
    public interface ICartLogic
    {
        public Task<List<CartProducts>> FetchCartProducts(string userId);

        public Task<bool> ItemPresentInCart(string userId, Guid productId);

        public Task AddItemToCart(Cart cart);

        public Task RemoveFromCart(string userId, Guid productId); 

        public Task UpdateQuantity(string userId, Guid productId, int quantity);
    }
}
