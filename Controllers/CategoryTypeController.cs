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
    public class CategoryTypeController : ControllerBase
    {
        private readonly CategoryTypeService _categoryTypeService;

        public CategoryTypeController(CategoryTypeService categoryTypeService)
        {
            _categoryTypeService = categoryTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryTypeDTO>>> GetCategoryTypes()
        {
            return await _categoryTypeService.GetCategoryTypesDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryTypeDTO>> GetCategoryTypeById(int id)
        {
            return await _categoryTypeService.GetCategoryTypeByIdDTO(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType categoryType)
        {
            return await _categoryTypeService.PostCategoryType(categoryType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryType>> EditCategoryType(int id, CategoryType categoryType)
        {
            if (id != categoryType.ID)
            {
                return BadRequest();
            }

            try
            {
                var categoryTypeReturn = await _categoryTypeService.EditCategoryType(categoryType);
                return categoryTypeReturn;
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _categoryTypeService.GetCategoryTypeByIdDTO(id);

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
        public async Task<ActionResult<CategoryType>> DeleteCategoryType(int id)
        {
            var categoryType = await _categoryTypeService.GetCategoryTypeByIdDTO(id);

            if (categoryType == null)
            {
                return NotFound();
            }

            return await _categoryTypeService.DeleteCategoryType(id);
        }

    }
}
