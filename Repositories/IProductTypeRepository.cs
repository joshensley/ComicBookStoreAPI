using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IProductTypeRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<ProductType, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductType, TResult>> selector);

        Task<ActionResult<ProductType>> Post(ProductType productType);

        Task<ActionResult<ProductType>> Edit(ProductType productType);

        Task<ActionResult<ProductType>> Delete(int id);
    }
}
