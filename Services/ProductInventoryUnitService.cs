using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Services
{
    public class ProductInventoryUnitService
    {
        private readonly IProductInventoryUnitRepository _productInventoryUnitRepository;

        public ProductInventoryUnitService(IProductInventoryUnitRepository productInventoryUnitRepository)
        {
            _productInventoryUnitRepository = productInventoryUnitRepository;
        }

        // GET: Get paginated list of ProductInventoryUnitDTO
        public async Task<ActionResult<PaginatedList<ProductInventoryUnitDTO>>> GetProductInventoryUnitsDTO(int pageNumber, string productName, string productType, string categoryType)
        {
            if (productName == null) productName = "";
            if (productType == null) productType = "";
            if (categoryType == null) categoryType = "";

            return await _productInventoryUnitRepository.Get(pageNumber, productName, productType, categoryType, ProductInventoryUnitDTO.ProductInventoryUnitSelector);
        }

        // GET: Get ProductInventoryUnitDTO by Id
        public async Task<ActionResult<ProductInventoryUnitDTO>> GetProductInventoryUnitByIdDTO(int id)
        {
            return await _productInventoryUnitRepository.GetById(id, ProductInventoryUnitDTO.ProductInventoryUnitSelector);
        }

        // POST: Post ProductInventoryUnits
        public async Task<ActionResult<bool>> PostProductInventoryUnits(ProductInventoryUnit productInventoryUnit, int quantity)
        {
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;

            var productInventoryUnitList = new List<ProductInventoryUnit>() { };
            for (int i = 0; i < quantity; i++)
            {
                var productInventoryUnitLoop = new ProductInventoryUnit()
                {
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt,
                    InStock = productInventoryUnit.InStock,
                    ProductID = productInventoryUnit.ProductID
                };

                productInventoryUnitList.Add(productInventoryUnitLoop);
            }

            return await _productInventoryUnitRepository.Post(productInventoryUnitList);
        }

        // DELETE: Delete ProductInventoryUnit
        public async Task<ActionResult<bool>> DeleteProductInventoryUnit(int id)
        {
            return await _productInventoryUnitRepository.Delete(id);
        }
    }
}
