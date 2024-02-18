using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using DataAccessLayer.DataAccessServices.CartDataService;
using DataAccessLayer.DataAccessServices.OrdersDataService;
using DataAccessLayer.DataAccessServices.ProductDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.OrdersLogicService
{
    public class OrdersLogic: IOrdersLogic
    {
        private readonly IOrdersData _ordersData;
        private readonly IProductData _productData;
        private readonly ICartData _cartData;

        public OrdersLogic(IOrdersData ordersData, ICartData cartData, IProductData productData)
        {
            _ordersData = ordersData;
            _productData = productData;
            _cartData = cartData;
        }

        public async Task PlaceOrder(string userId)
        {
            var cartItems = _cartData.FetchCartProducts(userId);

            foreach (var cartItem in cartItems)
            {
                Orders item = new Orders
                {
                    OrderId = Guid.NewGuid(),
                    Email = cartItem.Email,
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    DateOfOrder = DateTime.Now.Date,
                };

                await _productData.UpdateProductQuantity(cartItem, cartItem.Quantity);
                await _ordersData.AddProductToOrders(item);
                await _cartData.RemoveCartItem(cartItem.Email, cartItem.ProductId);
            }
        }

        public List<Orders> FetchOrders(string userId)
        {
            var reqProducts = _ordersData.FetchOrders(userId);
            return reqProducts;
        }
    }
}
