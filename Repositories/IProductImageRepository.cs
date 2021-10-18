using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IProductImageRepository
    {
        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductImage, TResult>> selector);

        Task<ActionResult<List<TResult>>> GetByProductId<TResult>(int productId, Expression<Func<ProductImage, TResult>> selector);

        Task<ActionResult<ProductImage>> Post(ProductImage productImage);

        Task<ActionResult<int>> Delete(int id);
    }
}
