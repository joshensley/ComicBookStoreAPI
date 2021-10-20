using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Models.Other;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Services;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoryUnitController : ControllerBase
    {
        private readonly ProductInventoryUnitService _productInventoryUnitService;

        public ProductInventoryUnitController(ProductInventoryUnitService productInventoryUnitService)
        {
            _productInventoryUnitService = productInventoryUnitService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchProductInventoryUnit>> GetProductInventoryUnits(
            int pageNumber, 
            string productName, 
            string productType, 
            string categoryType)
        {
            var productInventoryUnits = (await _productInventoryUnitService.GetProductInventoryUnitsDTO(pageNumber, productName, productType, categoryType)).Value;

            var searchProductInventoryUnits = new SearchProductInventoryUnit()
            {
                ProductInventoryUnitList = productInventoryUnits,
                PageIndex = productInventoryUnits.PageIndex,
                TotalPages = productInventoryUnits.TotalPages,
                HasNextPage = productInventoryUnits.HasNextPage,
                HasPreviousPage = productInventoryUnits.HasPreviousPage
            };

            return searchProductInventoryUnits;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventoryUnitDTO>> GetProductInventoryUnitByIdDTO(int id)
        {
            var productInventoryUnit = await _productInventoryUnitService.GetProductInventoryUnitByIdDTO(id);

            return productInventoryUnit;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostProductInventoryUnits(ProductInventoryUnit productInventoryUnit, int quantity)
        {
            if (ModelState.IsValid && quantity > 0)
            {
                try
                {
                    var productInventoryUnits = await _productInventoryUnitService.PostProductInventoryUnits(productInventoryUnit, quantity);

                    return productInventoryUnits;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProductInventoryUnit(int id)
        {
            var productInventoryUnit = await _productInventoryUnitService.GetProductInventoryUnitByIdDTO(id);

            if (productInventoryUnit == null)
            {
                return NotFound();
            }

            var isDeleted = (await _productInventoryUnitService.DeleteProductInventoryUnit(id)).Value;

            if (isDeleted == false)
            {
                return NotFound();
            }

            return isDeleted;
        }
    }
}
