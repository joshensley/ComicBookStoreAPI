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
    public class ProductSpecificationValueService
    {
        public readonly IProductSpecificationValueRepository _productSpecificationValueRepository;

        public ProductSpecificationValueService(IProductSpecificationValueRepository productSpecificationValueRepository)
        {
            _productSpecificationValueRepository = productSpecificationValueRepository;
        }

        // POST: Post blank Product Specification Values
        public async Task<ActionResult<bool>> PostBlankProductSpecificationValues(int productSpecificationId, List<ProductDTO> products)
        {

            var productSpecificationValues = new List<ProductSpecificationValue>() { };
            foreach (var item in products)
            {
                var productSpecificationValue = new ProductSpecificationValue()
                {
                    Value = "",
                    ProductSpecificationID = productSpecificationId,
                    ProductID = item.ID
                };

                productSpecificationValues.Add(productSpecificationValue);
            }

            return await _productSpecificationValueRepository.PostBlankProductSpecificationValues(productSpecificationValues);

        }

        // EDIT: Edit Product Specification Values
        public async Task<ActionResult<List<ProductSpecificationValue>>> EditProductSpecificationValues(ProductDTO productDTO)
        {

            var productSpecificationValues = new List<ProductSpecificationValue>() { };

            foreach (var item in productDTO.ProductSpecificationNameValues)
            {
                var productSpecificationValue = new ProductSpecificationValue()
                {
                    ID = item.ProductSpecificationValueID,
                    ProductID = item.ProductID,
                    ProductSpecificationID = item.ProductSpecificationID,
                    Value = item.Value
                };

                productSpecificationValues.Add(productSpecificationValue);
            }

            return await _productSpecificationValueRepository.Edit(productSpecificationValues);
        }

        // DELETE: Delete Product Specification Values by ProductSpecificationID
        public async Task<ActionResult<bool>> DeleteProductSpecificationValues(int productSpecificationId)
        {
            return await _productSpecificationValueRepository.DeleteRange(productSpecificationId);
        }
    }
}
