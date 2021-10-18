using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models
{
    public class ProductType
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<ProductSpecification> ProductSpecifications { get; set; }
    }
}
