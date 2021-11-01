using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models
{
    public class Cart
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Application User ID")]
        public string ApplicationUserID { get; set; }

        [Required]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
