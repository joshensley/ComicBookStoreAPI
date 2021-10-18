using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class ProductImageDTO
    {
        public int ID { get; set; }
        public string ImageTitle { get; set; }
        public string AuthorizedImageURL { get; set; }
        public string AlternateText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public static Expression<Func<ProductImage, ProductImageDTO>> ProductImageSelector
        {
            get
            {
                return productImage => new ProductImageDTO()
                {
                    ID = productImage.ID,
                    ImageTitle = productImage.ImageTitle,
                    AuthorizedImageURL = productImage.AuthorizedImageURL,
                    AlternateText = productImage.AlternateText,
                    CreatedAt = productImage.CreatedAt,
                    UpdatedAt = productImage.UpdatedAt,
                    ProductID = productImage.ProductID,
                    ProductName = productImage.Product.Name
                };
            }
        }

    }
}
