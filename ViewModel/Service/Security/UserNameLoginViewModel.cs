using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserNameLoginViewModel
    {
        /// <summary>
        /// Username - Nvarchar(100)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        public string Password { get; set; }

        public Int64 TenantId { get; set; }
    }
}
