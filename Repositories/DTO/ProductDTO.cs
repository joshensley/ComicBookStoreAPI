using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Models.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Regular Price")]
        public decimal RegularPrice { get; set; }

        [Display(Name = "Discount Price")]
        public decimal DiscountPrice { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Category Type ID")]
        public int CategoryTypeID { get; set; }

        [Display(Name = "Category Type")]
        public string CategoryType { get; set; }

        [Display(Name = "Product Type ID")]
        public int ProductTypeID { get; set; }

        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        public IEnumerable<ProductSpecificationNameValue> ProductSpecificationNameValues { get; set; }

        public static Expression<Func<Product, ProductDTO>> ProductSelector
        {
            get
            {
                return product => new ProductDTO()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Description = product.Description,
                    RegularPrice = product.RegularPrice,
                    DiscountPrice = product.DiscountPrice,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    CategoryTypeID = product.CategoryTypeID,
                    CategoryType = product.CategoryType.Name,
                    ProductTypeID = product.ProductTypeID,
                    ProductType = product.ProductType.Name
                };
            }
        }

        public static Expression<Func<Product, ProductDTO>> ProductWithSpecificationValuesSelector
        {
            get
            {
                return product => new ProductDTO()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Description = product.Description,
                    RegularPrice = product.RegularPrice,
                    DiscountPrice = product.DiscountPrice,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    CategoryTypeID = product.CategoryTypeID,
                    CategoryType = product.CategoryType.Name,
                    ProductTypeID = product.ProductTypeID,
                    ProductType = product.ProductType.Name,
                    ProductSpecificationNameValues = product.ProductSpecificationValues
                        .Select(p => new ProductSpecificationNameValue()
                        {
                            ProductSpecificationValueID = p.ID,
                            ProductID = p.ProductID,
                            ProductSpecificationID = p.ProductSpecificationID,
                            Name = p.ProductSpecification.Name,
                            Value = p.Value
                        })
                };
            }
        }
    }
}
