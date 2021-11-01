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
    }
}
