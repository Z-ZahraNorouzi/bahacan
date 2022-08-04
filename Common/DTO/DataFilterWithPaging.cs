
using System.Collections.Generic;

namespace Common.DTO
{
    public class DataFilterWithPaging
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public Filter[] Filters { get; set; }

        public string OrderBy { get; set; }
    }
}