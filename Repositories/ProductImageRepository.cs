using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        public readonly ApplicationDbContext _db;

        public ProductImageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductImage, TResult>> selector)
        {
            var productImage = await _db.ProductImages
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productImage;
        }

        public async Task<ActionResult<List<TResult>>> GetByProductId<TResult>(int productId, Expression<Func<ProductImage, TResult>> selector)
        {
            var productImages = await _db.ProductImages
                .Where(p => p.ProductID == productId)
                .Select(selector)
                .ToListAsync();

            return productImages;
        }

        public async Task<ActionResult<ProductImage>> Post(ProductImage productImage)
        {
            _db.ProductImages.Add(productImage);
            await _db.SaveChangesAsync();
            return productImage;
        }

        public async Task<ActionResult<int>> Delete(int id)
        {
            var productImage = await _db.ProductImages.Where(p => p.ID == id).FirstOrDefaultAsync();

            _db.ProductImages.Remove(productImage);
            await _db.SaveChangesAsync();
            return id;
        }
    }
}
