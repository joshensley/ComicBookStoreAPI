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
    public class ProductSpecificationService
    {
        public readonly IProductSpecificationRepository _productSpecificationRepository;

        public ProductSpecificationService(IProductSpecificationRepository productSpecificationRepository)
        {
            _productSpecificationRepository = productSpecificationRepository;
        }

        // GET: Get product specification by id
        public async Task<ActionResult<ProductSpecificationDTO>> GetProductSpecificationByIdDTO(int id)
        {
            return await _productSpecificationRepository.GetById(id, ProductSpecificationDTO.ProductSpecificationSelector);
        }

        // POST: Post product specification
        public async Task<ActionResult<ProductSpecification>> PostProductSpecification(ProductSpecification productSpecification)
        {
            return await _productSpecificationRepository.Post(productSpecification);
        }

        // EDIT: Edit product specifications
        public async Task<ActionResult<ProductType>> EditProductSpecifications(ProductType productType)
        {
            return await _productSpecificationRepository.Edit(productType);
        }

        // DELETE: Delete product specification
        public async Task<ActionResult<ProductSpecification>> DeleteProductSpecification(int id)
        {
            return await _productSpecificationRepository.Delete(id);
        }
    }
}
