using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class ResultModel<T>
    {
        public Boolean Success { get; set; }

        public string? Error { get; set; }
        
        public T? Result { get; set; }

        public IList<T>? Items { get; set; }

        public Int64 TotalCount {get; set;}
    }
}