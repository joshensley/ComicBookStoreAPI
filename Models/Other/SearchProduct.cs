using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models.Other
{
    public class SearchProduct
    {
        public PaginatedList<ProductDTO> ProductList { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
