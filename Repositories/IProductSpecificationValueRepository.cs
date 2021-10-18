using ComicBookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    public interface IProductSpecificationValueRepository
    {
        Task<ActionResult<bool>> PostBlankProductSpecificationValues(List<ProductSpecificationValue> productSpecificationValues);

        Task<ActionResult<List<ProductSpecificationValue>>> Edit(List<ProductSpecificationValue> productSpecificationValue);

        Task<ActionResult<bool>> DeleteRange(int productSpecificationId);
    }
}
