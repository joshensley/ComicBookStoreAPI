using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IProductsRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<Product, TResult>> selector);

        Task<ActionResult<List<TResult>>> GetByProductTypeId<TResult>(int productTypeId, Expression<Func<Product, TResult>> selector);

        Task<ActionResult<PaginatedList<TResult>>> GetSearchProducts<TResult>(
            string search, 
            Expression<Func<Product, TResult>> selector, 
            int? pageNumber,
            string categoryType,
            string productType);

        Task<ActionResult<Product>> Post(Product product);

        Task<ActionResult<Product>> Edit(Product product);

        Task<ActionResult<Product>> Delete(int id);
    }
}
