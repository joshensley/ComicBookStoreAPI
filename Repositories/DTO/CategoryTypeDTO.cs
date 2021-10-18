using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class CategoryTypeDTO
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public static Expression<Func<CategoryType, CategoryTypeDTO>> CatetoryTypeSelector
        {
            get
            {
                return categoryType => new CategoryTypeDTO()
                {
                    ID = categoryType.ID,
                    Name = categoryType.Name,
                    IsActive = categoryType.IsActive
                };
            }
        }

    }
}
