using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ProductSpecificationDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        public static Expression<Func<ProductSpecification, ProductSpecificationDTO>> ProductSpecificationSelector
        {
            get
            {
                return productSpecification => new ProductSpecificationDTO()
                {
                    ID = productSpecification.ID,
                    Name = productSpecification.Name,
                    ProductTypeID = productSpecification.ProductTypeID,
                    //ProductType = productSpecification.ProductType
                };
            }
        }
    }
}
