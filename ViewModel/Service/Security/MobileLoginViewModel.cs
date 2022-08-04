using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class MobileLoginViewModel
    {
        /// <summary>
        /// Username - Nvarchar(100)
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        public string ActiveCode { get; set; }

        public Int64 TenantId { get; set; }
    }
}