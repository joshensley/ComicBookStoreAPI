using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Models.Error;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly ProductsService _productsService;
        private readonly ApplicationUserService _applicationUserService;

        public CartController(
            CartService cartService, 
            ProductsService productsService,
            ApplicationUserService applicationUserService)
        {
            _cartService = cartService;
            _productsService = productsService;
            _applicationUserService = applicationUserService;
        }

        [HttpGet("{applicationUserId}")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetApplicationUserCart(string applicationUserId)
        {
            return await _cartService.GetApplicationUserCartDTO(applicationUserId);
        }

        [HttpGet("Details/{applicationUserId}")]
        public async Task<ActionResult<List<CartDTO>>> GetApplicationUserCartDetail(string applicationUserId)
        {
            return await _cartService.GetApplicationUserCartDetailDTO(applicationUserId);
        }

        [HttpPost]
        public async Task<ActionResult<CartDTO>> AddProductToApplicationUserCart(int productId, string applicationUserId)
        {
            // If user exists
            var applicationUser = (await _applicationUserService.GetApplicationUserDTO(applicationUserId)).Value;
            if (applicationUser == null) return BadRequest();

            // If product exists
            var product = (await _productsService.GetProductByIdDTO(productId)).Value;
            if (product == null) return BadRequest();

            // If product is not in user cart
            var productInCart = (await _cartService.GetProductFromApplicationUserCart(applicationUserId, productId)).Value;
            if (productInCart != null) return BadRequest();

            // Post product to application user cart
            await _cartService.PostProductToApplicationUserCart(applicationUserId, productId);

            var newProductInApplicationUserCart = (await _cartService.GetProductFromApplicationUserCart(applicationUserId, productId)).Value;

            return newProductInApplicationUserCart;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<CartDTO>>> EditProductInApplicationUserCart(int id, Cart cart)
        {
            if (ModelState.IsValid)
            {
                // matching ids
                if (id == cart.ID)
                {
                    // check if requested quantity is equal to or less than 1
                    if (cart.Quantity < 1) 
                    {
                        var error = new Error()
                        {
                            Type = "error",
                            Description = "Product in cart must have at least 1 quantity"
                        };

                        return BadRequest(error);
                    }

                    // check if product in cart exists
                    var cartProductExists = (await _cartService.GetCartProductByIdReturnCartModel(id)).Value;
                    if (cartProductExists == null) 
                    {
                        var error = new Error()
                        {
                            Type = "error",
                            Description = "Product in cart does not exist"
                        };

                        return BadRequest(error);
                    }
                    

                    // get inventory quantity for product, and then compare inventory quantity and requested quantity
                    var product = (await _productsService.GetProductByIdDTO(cart.ProductID)).Value;
                    if (cart.Quantity > product.InventoryStock) {

                        var error = new Error()
                        {
                            Type = "error",
                            Description = $"{product.Name} has {product.InventoryStock} inventory in stock"
                        };

                        return BadRequest(error);
                    };

                    // edit the quantity of cart product
                    var updatedCart = (await _cartService.EditProductInApplicationUserCart(cart)).Value;

                    // return list CartDTO
                    return await _cartService.GetApplicationUserCartDetailDTO(updatedCart.ApplicationUserID);
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteProductInApplicationUserCart(int id)
        {
            var product = (await _cartService.GetCartProductByIdReturnCartModel(id)).Value;
            if (product == null) return BadRequest();

            return await _cartService.DeleteProductInApplicationUserCart(product);
        }
    }
}
