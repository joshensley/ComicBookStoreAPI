using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Models.Other
{
    public class ProductSpecificationNameValue
    {
        public int ProductSpecificationValueID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductSpecificationID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
