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
    public class CategoryTypeService
    {
        public readonly ICategoryTypeRepository _categoryTypeRepository;

        public CategoryTypeService(ICategoryTypeRepository categoryTypeRepository)
        {
            _categoryTypeRepository = categoryTypeRepository;
        }

        // GET: all category types
        public async Task<ActionResult<IEnumerable<CategoryTypeDTO>>> GetCategoryTypesDTO()
        {
            return await _categoryTypeRepository.Get(CategoryTypeDTO.CatetoryTypeSelector);
        }

        // GET: category type by id
        public async Task<ActionResult<CategoryTypeDTO>> GetCategoryTypeByIdDTO(int id)
        {
            return await _categoryTypeRepository.GetById(id, CategoryTypeDTO.CatetoryTypeSelector);
        }

        // POST: post category type
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType categoryType)
        {
            return await _categoryTypeRepository.Post(categoryType);
        }

        // EDIT: edit category type
        public async Task<ActionResult<CategoryType>> EditCategoryType(CategoryType categoryType)
        {
            return await _categoryTypeRepository.Edit(categoryType);
        }

        // DELETE: delete category type
        public async Task<ActionResult<CategoryType>> DeleteCategoryType(int id)
        {
            return await _categoryTypeRepository.Delete(id);
        }
    }
}
