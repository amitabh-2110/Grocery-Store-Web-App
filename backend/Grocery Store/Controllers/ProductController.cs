using BusinessLogicLayer.LogicServices.ProductLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store.Controllers
{
    [Route("api/[controller]"), AllowAnonymous]
    [ApiController]
    public class ProductController: Controller
    {
        private readonly IProductLogic _productLogic;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductLogic productLogic, IWebHostEnvironment environment)
        {
            _productLogic = productLogic;
            _environment = environment;
        }

        [NonAction]
        public string FetchPath(string productId)
        {
            string path = Path.Combine(_environment.WebRootPath, "Uploads", "Products", productId);
            return path;
        }

        [NonAction]
        public string FetchImage(string productId)
        {
            string path = FetchPath(productId);
            string imagePath = Path.Combine(path, "image.jpg");
            string hostUrl = "https://localhost:7272";
            string imageUrl;

            if (!System.IO.File.Exists(imagePath))
            {
                imageUrl = Path.Combine(hostUrl, "Uploads", "Common", "no-image.jpg");
            }
            else
            {
                imageUrl = Path.Combine(hostUrl, "Uploads", "Products", productId, "image.jpg");
            }

            return imageUrl;
        }

        [NonAction]
        public List<ImageInfo> FetchImages(List<Products> products)
        {
            var images = new List<ImageInfo>();

            products.ForEach(prod =>
            {
                string prodId = prod.ProductId.ToString() ?? "";
                string image = FetchImage(prodId);
                ImageInfo imgProd = new ImageInfo
                {
                    ProductId = prod.ProductId ?? Guid.NewGuid(),
                    ImageUrl = image,
                };
                images.Add(imgProd);
            });

            return images;
        }

        [HttpGet]
        [Route("getAllProducts")]
        public async Task<ActionResult> FetchAllProducts()
        {
            var products = await _productLogic.FetchProducts();
            var images = FetchImages(products);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            FetchProductAndImage fetchProductAndImage = new FetchProductAndImage
            {
                Product = products,
                ImageData = images
            };

            return Ok(new { fetchProductAndImage.Product, fetchProductAndImage.ImageData });
        }

        [HttpGet]
        [Route("getProductsByCategorySearch")]
        public async Task<ActionResult> FetchProductsByCategorySearch(string categoryId, string? search)
        {
            var products = await _productLogic.FetchProductsByCategorySearch(categoryId, search ?? "");
            var images = FetchImages(products);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            FetchProductAndImage fetchProductAndImage = new FetchProductAndImage
            {
                Product = products,
                ImageData = images
            };

            return Ok(new { fetchProductAndImage.Product, fetchProductAndImage.ImageData });
        }

        [HttpGet]
        [Route("getProductById/productId")]
        public async Task<ActionResult> FetchProductById(string productId)
        {
            var products = await _productLogic.FetchProductsById(new Guid(productId));
            var images = FetchImages(products);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            FetchProductAndImage fetchProductAndImage = new FetchProductAndImage
            {
                Product = products,
                ImageData = images
            };

            return Ok(new { fetchProductAndImage.Product, fetchProductAndImage.ImageData });
        }
    }
}
