using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ProductTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ProductTypeID { get; set; }
        public IEnumerable<ProductSpecification> ProductSpecifications { get; set; }

        public static Expression<Func<ProductType, ProductTypeDTO>> ProductTypeSelector
        {
            get
            {
                return productType => new ProductTypeDTO()
                {
                    ID = productType.ID,
                    Name = productType.Name,
                    IsActive = productType.IsActive
                };
            }
        }

        public static Expression<Func<ProductType, ProductTypeDTO>> ProductTypeWithProductSpecificationsSelector
        {
            get
            {
                return productType => new ProductTypeDTO()
                {
                    ID = productType.ID,
                    Name = productType.Name,
                    IsActive = productType.IsActive,
                    ProductSpecifications = productType.ProductSpecifications
                        .Select(p => new ProductSpecification()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            ProductTypeID = p.ProductTypeID
                        })
                };
            }
        }
    }
}
