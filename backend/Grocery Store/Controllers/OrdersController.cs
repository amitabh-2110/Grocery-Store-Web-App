using BusinessLogicLayer.LogicServices.OrdersLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "User")]
    [ApiController]
    public class OrdersController: Controller
    {
        private readonly IOrdersLogic _ordersLogic;
        private readonly IWebHostEnvironment _environment;

        public OrdersController(IOrdersLogic ordersLogic, IWebHostEnvironment environment)
        {
            _ordersLogic = ordersLogic;
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
        public List<ImageInfoOrders> FetchImages(List<Orders> products)
        {
            var images = new List<ImageInfoOrders>();

            products.ForEach(prod =>
            {
                string prodId = prod.ProductId.ToString() ?? "";
                string image = FetchImage(prodId);
                ImageInfoOrders imgProd = new ImageInfoOrders
                {
                    OrderId = prod.OrderId,
                    ProductId = prod.ProductId,
                    ImageUrl = image,
                };
                images.Add(imgProd);
            });

            return images;
        }

        [HttpGet]
        [Route("placeOrder")]
        public async Task<ActionResult> PlaceOrder(string userId)
        {
            await _ordersLogic.PlaceOrder(userId);

            return Ok();
        }

        [HttpGet]
        [Route("getOrders")]
        public ActionResult FetchOrdersPlaced(string userId)
        {
            if(userId == null || !ModelState.IsValid) 
                return BadRequest(ModelState);

            var products = _ordersLogic.FetchOrders(userId);
            var images = FetchImages(products);

            FetchOrderProducts fetchOrderProducts = new FetchOrderProducts
            {
                Products = products,
                ImageData = images
            };

            return Ok(new { fetchOrderProducts.Products, fetchOrderProducts.ImageData });
        }
    }
}
