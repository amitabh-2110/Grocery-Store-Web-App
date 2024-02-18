using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.OrdersDataService
{
    public interface IOrdersData
    {
        public Task AddProductToOrders(Orders model);

        public List<Orders> FetchOrders(string userId);
    }
}
