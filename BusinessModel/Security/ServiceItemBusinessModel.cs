using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class ServiceItemBusinessModel : BusinessModelBase
    {
        public long? ServiceItemId { get; set; }
        public int ServiceId { get; set; }
        public long TenantId { get; set; }
        public long EntityId { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? DeletedById { get; set; }

        public virtual ServiceItemBusinessModel? Service { get; set; } = null!;
        public virtual TenantBusinessModel? Tenant { get; set; } = null!;
    }
}
