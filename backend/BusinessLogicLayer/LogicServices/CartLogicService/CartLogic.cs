using BusinessLogicLayer.LogicServices.ProductLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using DataAccessLayer.DataAccessServices.CartDataService;
using DataAccessLayer.DataAccessServices.ProductDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.CartLogicService
{
    public class CartLogic: ICartLogic
    {
        private readonly ICartData _cartData;
        private readonly IProductData _productData;

        public CartLogic(ICartData cartData, IProductData productData)
        {
            _cartData = cartData;
            _productData = productData;
        }

        public async Task<List<CartProducts>> FetchCartProducts(string userId)
        {
            var cartproducts = _cartData.FetchCartProducts(userId);
            var products = await _productData.FetchProducts();

            List<CartProducts> userCartProducts = new List<CartProducts>();

            cartproducts.ForEach(cartproduct =>
            {
                products.ForEach(prod =>
                {
                    if(cartproduct.ProductId == prod.ProductId)
                    {
                        CartProducts product = new CartProducts
                        {
                            CartQuantity = cartproduct.Quantity,
                            CartPrice = cartproduct.Price,
                            ProductId = cartproduct.ProductId,
                            ProductName = prod.ProductName,
                            ProductDescription = prod.Description,
                            ProductPrice = prod.Price,
                            AvailableQuantity = prod.AvailableQuantity,
                        };
                        userCartProducts.Add(product);
                    }
                });
            });

            return userCartProducts;
        }

        public async Task<bool> ItemPresentInCart(string userId, Guid productId)
        {
            bool isPresent = await _cartData.ItemPresentInCart(userId, productId);
            return isPresent;
        }

        public async Task AddItemToCart(Cart cart)
        {
            await _cartData.AddCartItem(cart);
        }

        public async Task RemoveFromCart(string userId, Guid productId)
        {
            await _cartData.RemoveCartItem(userId, productId);
        }

        public async Task UpdateQuantity(string userId, Guid productId, int quantity)
        {
            await _cartData.UpdateQuantity(userId, productId, quantity);
        }
    }
}
