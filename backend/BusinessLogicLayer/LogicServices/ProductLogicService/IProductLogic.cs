using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.ProductLogicService
{
    public interface IProductLogic
    {
        public Task<List<Products>> FetchProducts();

        public Task<List<Products>> FetchProductsByCategorySearch(string categoryId, string search);

        public Task<List<Products>> FetchProductsById(Guid productId);

        public Task AddProduct(Products model);

        public Task UpdateProduct(Products model);

        public Task<List<Products>> DeleteProduct(Guid productId);
    }
}
