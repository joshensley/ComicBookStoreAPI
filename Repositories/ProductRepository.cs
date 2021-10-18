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
    public class ProductRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector)
        {
            var products = await _db.Products
                .Select(selector)
                .ToListAsync();

            return products;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<Product, TResult>> selector)
        {
            var product = await _db.Products
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<ActionResult<List<TResult>>> GetByProductTypeId<TResult>(int productTypeId, Expression<Func<Product, TResult>> selector)
        {
            var products = await _db.Products
                .Where(p => p.ProductTypeID == productTypeId)
                .Select(selector)
                .ToListAsync();

            return products;
        }

        public async Task<ActionResult<PaginatedList<TResult>>> GetSearchProducts<TResult>(
            string search, 
            Expression<Func<Product, TResult>> selector, 
            int? pageNumber,
            string categoryType,
            string productType)
        {
            if (search == null || search == "All") search = "";
            if (categoryType == null || categoryType == "All") categoryType = "";
            if (productType == null || productType == "All" ) productType = "";

            IQueryable<TResult> productsIQ = _db.Products
                .Where(p => p.Name.Contains(search))
                .Where(p => p.CategoryType.Name.Contains(categoryType))
                .Where(p => p.ProductType.Name.Contains(productType))
                .Select(selector);

            int pageSize = 3;
            return await PaginatedList<TResult>.CreateAsync(productsIQ, pageNumber ?? 1, pageSize);
        }

        public async Task<ActionResult<Product>> Post(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<ActionResult<Product>> Edit(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return product;
        }
    }
}
