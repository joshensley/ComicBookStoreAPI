using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public class ProductSpecificationValueRepository : IProductSpecificationValueRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductSpecificationValueRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<bool>> PostBlankProductSpecificationValues(List<ProductSpecificationValue> productSpecificationValues)
        {
            _db.ProductSpecificationValues.AddRange(productSpecificationValues);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<List<ProductSpecificationValue>>> Edit(List<ProductSpecificationValue> productSpecificationValue)
        {
            _db.ProductSpecificationValues.UpdateRange(productSpecificationValue);
            await _db.SaveChangesAsync();
            return productSpecificationValue;
        }

        public async Task<ActionResult<bool>> DeleteRange(int productSpecificationId)
        {
            var productSpecificationValues = await _db.ProductSpecificationValues
                .Where(p => p.ProductSpecificationID == productSpecificationId)
                .ToListAsync();

            _db.ProductSpecificationValues.RemoveRange(productSpecificationValues);
            await _db.SaveChangesAsync();

            return true;
            
        }
    }
}
