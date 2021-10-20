using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class ProductInventoryUnitRepository : IProductInventoryUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductInventoryUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<PaginatedList<TResult>>> Get<TResult>(
            int? pageNumber, 
            string productName, 
            string productType, 
            string categoryType, 
            Expression<Func<ProductInventoryUnit, TResult>> selector)
        {
            IQueryable<TResult> productInventoryUnits = _db.ProductInventoryUnits
                .Where(p => p.Product.Name.Contains(productName))
                .Where(p => p.Product.ProductType.Name.Contains(productType))
                .Where(p => p.Product.CategoryType.Name.Contains(categoryType))
                .Select(selector);

            int pageSize = 3;
            return await PaginatedList<TResult>.CreateAsync(productInventoryUnits, pageNumber ?? 1, pageSize);
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductInventoryUnit, TResult>> selector)
        {
            var productInventoryUnit = await _db.ProductInventoryUnits
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productInventoryUnit;
        }

        public async Task<ActionResult<bool>> Post(List<ProductInventoryUnit> productInventoryUnits)
        {
            _db.ProductInventoryUnits.AddRange(productInventoryUnits);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Delete(int id)
        {
            var productInventoryUnit = await _db.ProductInventoryUnits
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();

            _db.ProductInventoryUnits.Remove(productInventoryUnit);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
