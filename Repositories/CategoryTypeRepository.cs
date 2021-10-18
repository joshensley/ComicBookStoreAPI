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
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<CategoryType, TResult>> selector)
        {
            var categoryTypes = await _db.CategoryType
                .Select(selector)
                .ToListAsync();

            return categoryTypes;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<CategoryType, TResult>> selector)
        {
            var categoryType = await _db.CategoryType
                .Where(c => c.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return categoryType;
        }

        public async Task<ActionResult<CategoryType>> Post(CategoryType categoryType)
        {
            _db.CategoryType.Add(categoryType);
            await _db.SaveChangesAsync();

            return categoryType;
        }

        public async Task<ActionResult<CategoryType>> Edit(CategoryType categoryType)
        {
            _db.CategoryType.Update(categoryType);
            await _db.SaveChangesAsync();

            return categoryType;
        }

        public async Task<ActionResult<CategoryType>> Delete(int id)
        {
            var categoryType = await _db.CategoryType.FindAsync(id);

            _db.CategoryType.Remove(categoryType);
            await _db.SaveChangesAsync();

            return categoryType;
        }

    }
}
