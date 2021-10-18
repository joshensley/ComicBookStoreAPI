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
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
        public readonly ApplicationDbContext _db;

        public ProductSpecificationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductSpecification, TResult>> selector)
        {
            var productSpecification = await _db.ProductSpecifications
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productSpecification;
        }

        public async Task<ActionResult<ProductSpecification>> Post(ProductSpecification productSpecification)
        {
            _db.ProductSpecifications.Add(productSpecification);

            await _db.SaveChangesAsync();

            return productSpecification;
        }

        public async Task<ActionResult<ProductType>> Edit(ProductType productType)
        {
            _db.ProductSpecifications.UpdateRange(productType.ProductSpecifications);
            await _db.SaveChangesAsync();
            return productType;
        }

        public async Task<ActionResult<ProductSpecification>> Delete(int id)
        {
            var productSpecification = await _db.ProductSpecifications.FindAsync(id);

            _db.ProductSpecifications.Remove(productSpecification);
            await _db.SaveChangesAsync();

            return productSpecification;
        }

        
    }
}
