using BusinessObjectLayer.Data;
using BusinessObjectLayer.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccessServices.ProductDataService
{
    public class ProductData: IProductData
    {
        private readonly ManageDb _context;

        public ProductData(ManageDb context)
        {
            _context = context;
        }

        public async Task<List<Products>> FetchProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task StoreProduct(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Products product)
        {
            var productToUpdate = await _context.Products.FindAsync(product.ProductId);

            if(productToUpdate != null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Category = product.Category;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.AvailableQuantity = product.AvailableQuantity;
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductQuantity(Cart item, int quant)
        {
            var productToUpdate = await _context.Products.FindAsync(item.ProductId);

            if(productToUpdate != null)
            {
                productToUpdate.AvailableQuantity -= quant;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid productId)
        {
            var productToDelete = await _context.Products.FindAsync(productId);

            if(productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}
