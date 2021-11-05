using ComicBookStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Repositories.DTO
{
    public class CartDTO
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public string ApplicationUserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductTypeName { get; set; }
        public string ImageTitle { get; set; }
        public string ImageUrl { get; set; }

        public static Expression<Func<Cart, CartDTO>> CartSelector
        {
            get
            {
                return cart => new CartDTO()
                {
                    ID = cart.ID,
                    Quantity = cart.Quantity,
                    ApplicationUserID = cart.ApplicationUserID,
                    ProductID = cart.ProductID,
                    ProductName = cart.Product.Name,
                    Price = cart.Product.RegularPrice,
                    TotalPrice = cart.Product.RegularPrice * cart.Quantity,
                    ProductTypeName = cart.Product.ProductType.Name
                };
            }
        }

        public static Expression<Func<Cart, CartDTO>> CartDetailSelector
        {
            get
            {
                return cart => new CartDTO()
                {
                    ID = cart.ID,
                    Quantity = cart.Quantity,
                    ApplicationUserID = cart.ApplicationUserID,
                    ProductID = cart.ProductID,
                    ProductName = cart.Product.Name,
                    Price = cart.Product.RegularPrice,
                    TotalPrice = cart.Product.RegularPrice * cart.Quantity,
                    ProductTypeName = cart.Product.ProductType.Name,
                    ImageTitle = cart.Product.ProductImages.First().ImageTitle,
                    ImageUrl = ""
                };
            }
        }
    }
}
