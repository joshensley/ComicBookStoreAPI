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

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET: Get product from application user cart
        public async Task<ActionResult<CartDTO>> GetProductFromApplicationUserCart(string applicationUserId, int productId)
        {
            return await _cartRepository.GetProductFromApplicationUserCart(CartDTO.CartSelector, applicationUserId, productId);
        }

        // GET: Get application user cart
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetApplicationUserCartDTO(string applicationUserId)
        {
            return await _cartRepository.GetByApplicationUserId(CartDTO.CartSelector, applicationUserId);
        }

        // POST: Post product to application user cart
        public async Task<ActionResult<Cart>> PostProductToApplicationUserCart(string applicationUserId, int productId)
        {
            return await _cartRepository.Post(applicationUserId, productId);
        }
    }
}
