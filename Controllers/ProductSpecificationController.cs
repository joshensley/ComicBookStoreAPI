using ComicBookStoreAPI.Data;
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
    public class ProductSpecificationController : ControllerBase
    {
        private readonly ProductSpecificationService _productSpecificationService;
        private readonly ProductsService _productService;
        private readonly ProductSpecificationValueService _productSpecificationValueService;
        private readonly ApplicationDbContext _db;

        public ProductSpecificationController(
            ProductSpecificationService productSpecificationService,
            ProductsService productService,
            ProductSpecificationValueService productSpecificationValueService,
            ApplicationDbContext db)
        {
            _productSpecificationService = productSpecificationService;
            _productService = productService;
            _productSpecificationValueService = productSpecificationValueService;
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSpecificationDTO>> GetProductSpecificationById(int id)
        {
            return await _productSpecificationService.GetProductSpecificationByIdDTO(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProductSpecificationDTO>> PostProductSpecification(ProductSpecification productSpecification)
        {
            var response = await _productSpecificationService.PostProductSpecification(productSpecification);

            // Get products by product type id
            var products = await _productService.GetProductsByProductTypeId(response.Value.ProductTypeID);

            // Posts blank product specification values 
            await _productSpecificationValueService.PostBlankProductSpecificationValues(response.Value.ID, products.Value);
            
            return await _productSpecificationService.GetProductSpecificationByIdDTO(response.Value.ID);            
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductType>> EditProductSpecification(int id, ProductType productType)
        {
            if (id != productType.ID || productType == null)
            {
                return BadRequest();
            }

            try
            {
                var productTypeWithSpecifications = await _productSpecificationService.EditProductSpecifications(productType);
                return productTypeWithSpecifications;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductSpecification>> DeleteProductSpecification(int id)
        {

            var productSpecification = await _productSpecificationService.GetProductSpecificationByIdDTO(id);

            if (productSpecification == null)
            {
                return NotFound();
            }

            var isDeleted = (await _productSpecificationValueService.DeleteProductSpecificationValues(id)).Value;

            if (isDeleted == false)
            {
                return NotFound();
            }

            return await _productSpecificationService.DeleteProductSpecification(id);
        }
    }
}
