using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<TResult>> GetProductFromApplicationUserCart<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId, int productID)
        {
            var productInCart = await _db.Cart
                .Where(c => c.ApplicationUserID == applicationUserId && c.ProductID == productID)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productInCart;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> GetByApplicationUserId<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId)
        {
            var cart =  await _db.Cart
                .Where(c => c.ApplicationUserID == applicationUserId)
                .Select(selector)
                .ToListAsync();

            return cart;
        }

        public async Task<ActionResult<Cart>> GetCartProductByIdReturnCartModel(int id)
        {
            var cartProduct = await _db.Cart.AsNoTracking().FirstOrDefaultAsync(c => c.ID == id);
            return cartProduct;
        }

        public async Task<ActionResult<List<TResult>>> GetByApplicationUserDetailsId<TResult>(Expression<Func<Cart, TResult>> selector, string applicationUserId)
        {
            var cart = await _db.Cart
                .Where(c => c.ApplicationUserID == applicationUserId)
                .Select(selector)
                .ToListAsync();

            return cart;
        }

        public async Task<ActionResult<Cart>> Post(string applicationUserId, int productId)
        {
            var productCart = new Cart()
            {
                Quantity = 1,
                ApplicationUserID = applicationUserId,
                ProductID = productId
            };

            _db.Cart.Add(productCart);
            await _db.SaveChangesAsync();

            return productCart;
        }

        public async Task<ActionResult<Cart>> Edit(Cart cart)
        {
            _db.Cart.Update(cart);
            await _db.SaveChangesAsync();

            return cart;
        }

        public async Task<ActionResult<int>> Delete(Cart product)
        {
            _db.Cart.Remove(product);
            await _db.SaveChangesAsync();

            return product.ID;
        }

    }
}
