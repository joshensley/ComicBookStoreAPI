using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IProductSpecificationRepository
    {
        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductSpecification, TResult>> selector);

        Task<ActionResult<ProductSpecification>> Post(ProductSpecification productSpecification);

        Task<ActionResult<ProductType>> Edit(ProductType productType);

        Task<ActionResult<ProductSpecification>> Delete(int id);
    }
}
