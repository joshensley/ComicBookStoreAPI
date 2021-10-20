using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models
{
    public class ProductInventoryUnit
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " In Stock is required")]
        [Display(Name = "Is Stock")]
        public bool InStock { get; set; }

        [Required(ErrorMessage = "Created at is required")]
        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product is required")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
