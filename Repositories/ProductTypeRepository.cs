using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        public readonly ApplicationDbContext _db;

        public ProductTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<ProductType, TResult>> selector)
        {
            var productTypes = await _db.ProductTypes
                .Select(selector)
                .ToListAsync();

            return productTypes;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductType, TResult>> selector)
        {
            var productType = await _db.ProductTypes
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productType;
        }

        public async Task<ActionResult<ProductType>> Post(ProductType productType)
        {
            _db.ProductTypes.Add(productType);
            await _db.SaveChangesAsync();

            return productType;
        }

        public async Task<ActionResult<ProductType>> Edit(ProductType productType)
        {
            _db.ProductTypes.Update(productType);
            await _db.SaveChangesAsync();

            return productType;
        }

        public async Task<ActionResult<ProductType>> Delete(int id)
        {
            var productType = await _db.ProductTypes.FindAsync(id);

            _db.ProductTypes.Remove(productType);
            await _db.SaveChangesAsync();

            return productType;
        }
    }
}
