using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.DTO
{
    public class Filter
    {
        public string Property { get; set; }

        public FilterOperation Operation { get; set; }

        public string[] Values { get; set; }
    }
}