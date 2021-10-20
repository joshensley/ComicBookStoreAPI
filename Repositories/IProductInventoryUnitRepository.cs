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
    public interface IProductInventoryUnitRepository
    {
        Task<ActionResult<PaginatedList<TResult>>> Get<TResult>(
            int? pageNumber, 
            string productName, 
            string productType, 
            string categoryType, 
            Expression<Func<ProductInventoryUnit, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductInventoryUnit, TResult>> selector);

        Task<ActionResult<bool>> Post(List<ProductInventoryUnit> productInventoryUnits);

        Task<ActionResult<bool>> Delete(int id);
    }
}
