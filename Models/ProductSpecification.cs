using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models
{
    public class ProductSpecification
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        public ICollection<ProductSpecificationValue> ProductSpecificationValues { get; set; }
    }
}
