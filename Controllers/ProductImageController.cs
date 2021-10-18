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
    public class ProductImageController : ControllerBase
    {
        private readonly ProductImageService _productImageService;
        private readonly ProductsService _productsService;

        public ProductImageController(ProductImageService productImageService, ProductsService productsService)
        {
            _productImageService = productImageService;
            _productsService = productsService;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ProductImageDTO>>> GetProductImages(int productId)
        {
            try
            {
                var product = await _productsService.GetProductByIdDTO(productId);

                if (product == null)
                {
                    return BadRequest();
                }

                var productImages = await _productImageService.GetProductImages(productId);
                return productImages;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductImageDTO>> PostProductImage([FromForm] ProductImage productImage)
        {
            if (ModelState.IsValid && productImage.FileUpload.Length > 1)
            {
                try
                {
                    return await _productImageService.PostProductImage(productImage);

                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteProductImage(int id)
        {
            var productImage = (await _productImageService.GetProductImageById(id)).Value;

            return await _productImageService.DeleteProductImage(id, productImage);
        }
    }
}
