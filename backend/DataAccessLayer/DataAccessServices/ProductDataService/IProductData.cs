using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.ProductDataService
{
    public interface IProductData
    {
        public Task<List<Products>> FetchProducts();

        public Task StoreProduct(Products product);

        public Task UpdateProduct(Products product);

        public Task DeleteProduct(Guid productId);

        public Task UpdateProductQuantity(Cart item, int quantity);
    }
}
