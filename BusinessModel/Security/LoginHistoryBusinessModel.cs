using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class LoginHistoryBusinessModel : BusinessModelBase
    {
        public long? LoginHistoryId { get; set; }
        public long UserId { get; set; }
        public long TenantId { get; set; }
        public int LoginStatusId { get; set; }
        public string? UserHostAddress { get; set; }
        public Guid Token { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual LoginStatusBusinessModel? LoginStatus { get; set; } = null!;
        public virtual TenantBusinessModel? Tenant { get; set; } = null!;
    }
}
