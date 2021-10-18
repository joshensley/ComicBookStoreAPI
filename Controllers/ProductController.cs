using ComicBookStoreAPI.Data;
using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Models.Other;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Services;
using ComicBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Controllers
{
    /*[Authorize(Roles = UserRoles.Admin)]*/
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            return await _productsService.GetProductDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            return await _productsService.GetProductByIdDTO(id);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductDetailsById(int id)
        {
            return await _productsService.GetProductDetailsByIdDTO(id);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<SearchProduct>> GetSearchProducts(
            string search, 
            int pageNumber, 
            string categoryType,
            string productType)
        {
            var products = (await _productsService.GetSearchProductDTO(
                search: search, 
                pageNumber: pageNumber, 
                categoryType: categoryType,
                productType: productType
                )).Value;

            var searchProducts = new SearchProduct()
            {
                ProductList = products,
                PageIndex = products.PageIndex,
                TotalPages = products.TotalPages,
                HasPreviousPage = products.HasPreviousPage,
                HasNextPage = products.HasNextPage
            };

            return searchProducts;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return await _productsService.PostProduct(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> EditProduct(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            try
            {
                var productReturn = await _productsService.EditProduct(product);
                return productReturn;
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _productsService.GetProductByIdDTO(id);

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
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productsService.GetProductByIdDTO(id);

            if (product == null)
            {
                return NotFound();
            }

            return await _productsService.DeleteProduct(id);
        }
    }
}
