using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models.Other
{
    public class SearchProductInventoryUnit
    {
        public PaginatedList<ProductInventoryUnitDTO> ProductInventoryUnitList { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
