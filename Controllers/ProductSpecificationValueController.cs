using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationValueController : ControllerBase
    {
        private readonly ProductSpecificationValueService _productSpecificationValueService;

        public ProductSpecificationValueController(ProductSpecificationValueService productSpecificationValueService)
        {
            _productSpecificationValueService = productSpecificationValueService;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProductSpecificationValue>>> EditProductSpecificationValues(int id, ProductDTO productDTO)
        {
            if (id != productDTO.ID)
            {
                return BadRequest();
            }

            try
            {
                var productSpecificationValues = await _productSpecificationValueService.EditProductSpecificationValues(productDTO);
                return productSpecificationValues;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
