using BusinessObjectLayer.Data;
using BusinessObjectLayer.DatabaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grocery_Store
{
    public class Seed
    {
        private readonly ManageDb _context;

        public Seed(ManageDb context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if(!_context.RegisteredPersons.Any())
            {
                var person1 = new RegisteredPerson
                {
                    Email = "admin@store.com",
                    Role = "Admin",
                    FullName = "Admin",
                    PhoneNumber = "1234567890",
                    Password = "@a345678"
                };

                _context.RegisteredPersons.Add(person1);

                var person2 = new RegisteredPerson
                {
                    Email = "admincontrol@store.com",
                    Role = "Admin",
                    FullName = "AdminControl",
                    PhoneNumber = "1234567890",
                    Password = "@a345679"
                };

                _context.RegisteredPersons.Add(person2);

                _context.SaveChanges();
            }

            if(!_context.Products.Any())
            {
                var random = new Random();
                var products = new List<Products>();

                var categories = new List<string>
                {
                    "Electronics", "Clothing", "Cups", "Food"
                };

                for(int i = 0; i < 16; i++)
                {
                    var category = categories[random.Next(categories.Count)];

                    var product = new Products
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = $"Product {i+1}",
                        Category = category,
                        Description = $"Description for Product {i+1}",
                        AvailableQuantity = random.Next(10, 100),
                        Price = (decimal)random.NextDouble()*(100-10)+10,
                    };

                    products.Add(product);
                }

                _context.Products.AddRange(products);
                _context.SaveChanges(); 
            }
        }
    }
}
