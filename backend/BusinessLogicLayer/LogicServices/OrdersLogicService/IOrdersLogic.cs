using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.OrdersLogicService
{
    public interface IOrdersLogic
    {
        public List<Orders> FetchOrders(string userId);

        public Task PlaceOrder(string userId);
    }
}
