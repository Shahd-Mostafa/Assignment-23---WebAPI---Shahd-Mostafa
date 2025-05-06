using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SpecificationParameters
{
    public class ProductSpecificationParameters
    {
        const int maxPageSize = 10;
        const int defaultPageSize = 5;
        const int defaultPageIndex = 1;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSort? Sort { get; set; }
        public string? Search { get; set; }

        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ?maxPageSize: value;
        }
        public int PageIndex { get; set; } = defaultPageIndex;
    }
    public enum ProductSort
    {
        NameAsc=1,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
}
