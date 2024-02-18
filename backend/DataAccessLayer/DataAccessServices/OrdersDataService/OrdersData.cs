using BusinessObjectLayer.Data;
using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.OrdersDataService
{
    public class OrdersData: IOrdersData
    {
        private readonly ManageDb _context;

        public OrdersData(ManageDb context)
        {
            _context = context;
        }

        public async Task AddProductToOrders(Orders model)
        {
            await _context.Orders.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public List<Orders> FetchOrders(string userId)
        {
            var reqOrders = _context.Orders.Where(item => item.Email == userId).ToList();
            return reqOrders;
        }
    }
}
