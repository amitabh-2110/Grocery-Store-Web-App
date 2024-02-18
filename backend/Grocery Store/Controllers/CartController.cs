using BusinessLogicLayer.LogicServices.CartLogicService;
using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "User")]
    [ApiController]
    public class CartController: Controller
    {
        private readonly ICartLogic _cartLogic;
        private readonly IWebHostEnvironment _environment;

        public CartController(ICartLogic cartLogic, IWebHostEnvironment environment)
        {
            _cartLogic = cartLogic;
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
        public List<ImageInfo> FetchImages(List<CartProducts> products)
        {
            var images = new List<ImageInfo>();

            products.ForEach(prod =>
            {
                string prodId = prod.ProductId.ToString();
                string image = FetchImage(prodId);
                ImageInfo imgProd = new ImageInfo
                {
                    ProductId = prod.ProductId,
                    ImageUrl = image,
                };
                images.Add(imgProd);
            });

            return images;
        }

        [HttpGet]
        [Route("getCartItems")]
        public async Task<ActionResult> FetchCartItems(string userId)
        {
            List<CartProducts> products = await _cartLogic.FetchCartProducts(userId);
            var images = FetchImages(products);

            if(products.Count == 0)
            {
                ResponseModel responseModel = new ResponseModel
                {
                    status = "fail",
                    title = "No item found",
                };

                return Ok(responseModel);
            }

            FetchCartProducts fetchCartProducts = new FetchCartProducts
            {
                Products = products,
                Images = images,
            };

            return Ok(new { fetchCartProducts.Products, fetchCartProducts.Images, fetchCartProducts.status }); 
        }

        [HttpGet]
        [Route("getItemPresentInCart")]
        public async Task<ActionResult> ItemPresentInCart(string userId, string product)
        {
            Guid productId = new Guid(product);
            bool isPresent = await _cartLogic.ItemPresentInCart(userId, productId);

            return Ok(isPresent);
        }

        [HttpPost]
        [Route("addToCart")]
        public async Task<ActionResult> AddCartItem(Cart cartItem)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cartLogic.AddItemToCart(cartItem);

            return Ok(ModelState);
        }

        [HttpDelete]
        [Route("removeCartItem")]
        public async Task<ActionResult> RemoveCartItem(Cart cartItem)
        {
            Guid productId = cartItem.ProductId;
            string userId = cartItem.Email;

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            await _cartLogic.RemoveFromCart(userId, productId);

            return Ok(ModelState);
        }

        [HttpPut]
        [Route("updateQuantity")]
        public async Task<ActionResult> UpdateQuantity(string userId, string product, string quantity)
        {
            Guid productId = new Guid(product);
            int quant = int.Parse(quantity); 

            if(!ModelState.IsValid || quant < 0)
                return BadRequest(ModelState);

            await _cartLogic.UpdateQuantity(userId, productId, quant);

            return Ok(ModelState);
        }
    }
}
