using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using DataAccessLayer.DataAccessServices.ProductDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.LogicServices.ProductLogicService
{
    public class ProductLogic: IProductLogic
    {
        private readonly IProductData _productData;
        
        public ProductLogic(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<List<Products>> FetchProducts()
        {
            var products = await _productData.FetchProducts();
            return products;
        }

        public async Task<List<Products>> FetchProductsByCategorySearch(string categoryId, string search)
        {
            var products = await _productData.FetchProducts();

            var reqproducts = products.Where(product => 
                (categoryId == "All" || product.Category == categoryId) && product.ProductName.Contains(search))
                .ToList();

            return reqproducts;
        }

        public async Task<List<Products>> FetchProductsById(Guid productId)
        {
            var products = await _productData.FetchProducts();
            var reqproducts = products.Where(product => product.ProductId == productId).ToList();

            return reqproducts;
        }

        public async Task AddProduct(Products product)
        {
            product.ProductId = Guid.NewGuid();
            await _productData.StoreProduct(product);
        }

        public async Task UpdateProduct(Products product)
        {
            await _productData.UpdateProduct(product);
        }

        public async Task<List<Products>> DeleteProduct(Guid productId)
        {
            await _productData.DeleteProduct(productId);

            var productInfo = await FetchProducts();

            return productInfo;
        }
    }
}
