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
    public class ProductsService
    {
        public readonly IProductsRepository _productsRepository;
        public readonly ProductImageService _productImageService;

        public ProductsService(
            IProductsRepository productsRepository, 
            ProductImageService productImageService)
        {
            _productsRepository = productsRepository;
            _productImageService = productImageService;
        }

        // GET: Get all products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductDTO()
        {
            return await _productsRepository.Get(ProductDTO.ProductSelector);
        }

        // GET: Get product by id
        public async Task<ActionResult<ProductDTO>> GetProductByIdDTO(int id)
        {
            return await _productsRepository.GetById(id, ProductDTO.ProductSelector);
        }

        // GET: Get product details by id
        public async Task<ActionResult<ProductDTO>> GetProductDetailsByIdDTO(int id)
        {
            return await _productsRepository.GetById(id, ProductDTO.ProductWithSpecificationValuesSelector);
        }

        // GET: Get products by product type id
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByProductTypeId(int productTypeId)
        {
            return await _productsRepository.GetByProductTypeId(productTypeId, ProductDTO.ProductSelector);
        }

        // GET: Query products details
        public async Task<ActionResult<PaginatedList<ProductDTO>>> GetSearchProductDTO(
            string search, 
            int pageNumber, 
            string categoryType,
            string productType)
        {
            var products = await _productsRepository.GetSearchProducts(
                search: search, 
                selector: ProductDTO.ProductWithSpecificationValuesSelector, 
                pageNumber: pageNumber,
                categoryType: categoryType,
                productType: productType);

            for (int i = 0; i < products.Value.Count; i++)
            {
                var imageTitle = products.Value[i].ImageTitle;

                if (imageTitle != null)
                {
                    // get the image from Firebase
                    var imageUrl = (await _productImageService.GetImageForProduct(imageTitle)).Value;

                    // get set the image in Firebase
                    products.Value[i].ImageUrl = imageUrl;
                }
            }


            return products;
        }

        // POST: Post product
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            return await _productsRepository.Post(product);
        }

        // EDIT: Edit product
        public async Task<ActionResult<Product>> EditProduct(Product product)
        {
            return await _productsRepository.Edit(product);
        }

        // DELETE: Delete product
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            return await _productsRepository.Delete(id);
        }
    }
}
