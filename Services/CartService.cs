using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Services
{
    public class CartService
    {
        public readonly ICartRepository _cartRepository;
        public readonly ProductImageService _productImageService;

        public CartService(ICartRepository cartRepository, ProductImageService productImageService)
        {
            _cartRepository = cartRepository;
            _productImageService = productImageService;
        }

        // GET: Get product from application user cart dto
        public async Task<ActionResult<CartDTO>> GetProductFromApplicationUserCart(string applicationUserId, int productId)
        {
            return await _cartRepository.GetProductFromApplicationUserCart(CartDTO.CartSelector, applicationUserId, productId);
        }

        // GET: Get application user cart dto
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetApplicationUserCartDTO(string applicationUserId)
        {
            return await _cartRepository.GetByApplicationUserId(CartDTO.CartSelector, applicationUserId);
        }

        // Get: Get cart product by id from application user cart
        public async Task<ActionResult<Cart>> GetCartProductByIdReturnCartModel(int id)
        {
            return await _cartRepository.GetCartProductByIdReturnCartModel(id);
        }

        // GET: Get application user cart details
        public async Task<ActionResult<List<CartDTO>>> GetApplicationUserCartDetailDTO(string applicationUserId)
        {
            var cartProducts = await _cartRepository.GetByApplicationUserDetailsId(CartDTO.CartDetailSelector, applicationUserId);

            if (cartProducts != null)
            {
                for (int i = 0; i < cartProducts.Value.Count; i++)
                {
                    var imageTitle = cartProducts.Value[i].ImageTitle;

                    if (imageTitle != null)
                    {
                        var imageUrl = (await _productImageService.GetImageForProduct(imageTitle)).Value;

                        cartProducts.Value[i].ImageUrl = imageUrl;
                    }
                }
            }

            return cartProducts;
        }

        // POST: Post product to application user cart
        public async Task<ActionResult<Cart>> PostProductToApplicationUserCart(string applicationUserId, int productId)
        {
            return await _cartRepository.Post(applicationUserId, productId);
        }

        // EDIT: Edit product in application user cart
        public async Task<ActionResult<Cart>> EditProductInApplicationUserCart(Cart cart)
        {
            return await _cartRepository.Edit(cart);
        }

        // DELETE: Delete product in application user cart
        public async Task<ActionResult<int>> DeleteProductInApplicationUserCart(Cart product)
        {
            return await _cartRepository.Delete(product);
        }
    }
}
