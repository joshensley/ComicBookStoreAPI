using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Services
{
    public class ProductTypeService
    {
        public readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        // GET: Get all product types
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypesDTO()
        {
            return await _productTypeRepository.Get(ProductTypeDTO.ProductTypeSelector);
        }

        // GET: Get all product types with product specifications
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypesWithProductSpecificationsDTO()
        {
            return await _productTypeRepository.Get(ProductTypeDTO.ProductTypeWithProductSpecificationsSelector);
        }

        // GET: Get product type by id with product specifications
        public async Task<ActionResult<ProductTypeDTO>> GetProductTypeByIdWithProductSpecificationsDTO(int id)
        {
            return await _productTypeRepository.GetById(id, ProductTypeDTO.ProductTypeWithProductSpecificationsSelector);
        }

        // GET: Get product type by id
        public async Task<ActionResult<ProductTypeDTO>> GetProductTypeByIdDTO(int id)
        {
            return await _productTypeRepository.GetById(id, ProductTypeDTO.ProductTypeSelector);
        }

        // POST: Post product type
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            return await _productTypeRepository.Post(productType);
        }

        // EDIT: Edit product type
        public async Task<ActionResult<ProductType>> EditProductType(ProductType productType)
        {
            return await _productTypeRepository.Edit(productType);
        }

        // DELETE: Delete product type
        public async Task<ActionResult<ProductType>> DeleteProductType(int id)
        {
            return await _productTypeRepository.Delete(id);
        }
    }
}
