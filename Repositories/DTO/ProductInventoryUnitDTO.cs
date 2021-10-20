using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ProductInventoryUnitDTO
    {
        public int ID { get; set; }
        public bool InStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryType { get; set; }
        public string ProductType { get; set; }

        public static Expression<Func<ProductInventoryUnit, ProductInventoryUnitDTO>> ProductInventoryUnitSelector
        {
            get
            {
                return productInventoryUnit => new ProductInventoryUnitDTO()
                {
                    ID = productInventoryUnit.ID,
                    InStock = productInventoryUnit.InStock,
                    CreatedAt = productInventoryUnit.CreatedAt,
                    UpdatedAt = productInventoryUnit.UpdatedAt,
                    ProductID = productInventoryUnit.ProductID,
                    ProductName = productInventoryUnit.Product.Name,
                    CategoryType = productInventoryUnit.Product.CategoryType.Name,
                    ProductType = productInventoryUnit.Product.ProductType.Name
                };
            }
        }
    }
}
