using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService _productTypeService;

        public ProductTypeController(ProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypes()
        {
            return await _productTypeService.GetProductTypesDTO();
        }

        [HttpGet("ProductSpecifications")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypesWithProductSpecifications()
        {
            return await _productTypeService.GetProductTypesWithProductSpecificationsDTO();
        }

        [HttpGet("ProductSpecifications/{id}")]
        public async Task<ActionResult<ProductTypeDTO>> GetProductTypeByIdWithProductSpecifications(int id)
        {
            return await _productTypeService.GetProductTypeByIdWithProductSpecificationsDTO(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDTO>> GetProductTypeById(int id)
        {
            return await _productTypeService.GetProductTypeByIdDTO(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            return await _productTypeService.PostProductType(productType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductType>> EditProductType(int id, ProductType productType)
        {
            if (id != productType.ID)
            {
                return BadRequest();
            }

            try
            {
                var productTypeReturn = await _productTypeService.EditProductType(productType);
                return productTypeReturn;
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _productTypeService.GetProductTypeByIdDTO(id);

                if (exists == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductType>> DeleteProductType(int id)
        {
            var productType = await _productTypeService.GetProductTypeByIdDTO(id);

            if (productType == null)
            {
                return NotFound();
            }

            return await _productTypeService.DeleteProductType(id);
        }
    }
}
