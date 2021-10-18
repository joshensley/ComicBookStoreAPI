using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models
{
    public class ProductImage
    {
        public int ID { get; set; }

        [Display(Name = "Image Title")]
        public string ImageTitle { get; set; }

        [Display(Name = "Authorized Image URL")]
        public string AuthorizedImageURL { get; set; }

        [Display(Name = "Alternate Text")]
        public string AlternateText { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public IFormFile FileUpload { get; set; }

        [Required]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        
    }
}
