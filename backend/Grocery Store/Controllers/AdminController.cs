using BusinessLogicLayer.LogicServices.ProductLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController: Controller
    {
        private readonly IProductLogic _productLogic;
        private readonly IWebHostEnvironment _environment;

        public AdminController(IProductLogic productLogic, IWebHostEnvironment environment)
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
        public async void UploadImage(IFormFile image, Products product)
        {
            if (image.Length == 0 || image == null)
                return;

            string productId = product.ProductId.ToString() ?? "";
            var path = FetchPath(productId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string imgPath = Path.Combine(path, "image.jpg");
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }

            using var fileStream = new FileStream(imgPath, FileMode.Create);
            await image.CopyToAsync(fileStream);
        }

        [NonAction]
        public bool DeleteImage(string? productId)
        {
            var path = FetchPath(productId ?? "");
            if (!Directory.Exists(path))
            {
                return false;
            }

            Directory.Delete(path);
            return true;
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
        public async Task<List<ImageInfo>> FetchImages()
        {
            var products = await _productLogic.FetchProducts();
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

        [HttpPost]
        [Route("addProduct")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddProduct([FromForm] StoreProductAndImage model)
        {
            IFormFile image = model.ImageData;
            Products product = model.Product;

            if (image == null || image.Length == 0 || !ModelState.IsValid)
                return BadRequest("Failed to submit product information. Try again...");

            await _productLogic.AddProduct(product);
            UploadImage(image, product);

            ResponseModel res = new ResponseModel
            {
                status = "ok",
                title = "Successfull",
            };

            return Ok(new { res.status, res.title });
        }

        [HttpPut]
        [Route("updateProduct")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdateProduct([FromForm] StoreProductAndImage model)
        {
            IFormFile image = model.ImageData;
            Products product = model.Product;

            if (!ModelState.IsValid)
                return BadRequest("Failed to submit product information. Try again...");

            await _productLogic.UpdateProduct(product);
            UploadImage(image, product);

            ResponseModel responseModel = new ResponseModel
            {
                status = "ok",
                title = "success"
            };

            return Ok(new { responseModel.status, responseModel.title });
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<ActionResult> DeleteProduct(Products product)
        {
            if(product.ProductId != null)
            {
                var productId = product.ProductId;
                var products = await _productLogic.DeleteProduct(productId ?? Guid.NewGuid());
                DeleteImage(productId.ToString());

                var images = await FetchImages();

                FetchProductAndImage fetchProductAndImage = new FetchProductAndImage
                {
                    Product = products,
                    ImageData = images
                };

                return Ok(new { fetchProductAndImage.Product, fetchProductAndImage.ImageData });
            }

            return BadRequest(ModelState);
        }
    }
}
