using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class ServiceItem
    {
        public ServiceItem()
        {
            UserServiceItems = new HashSet<UserServiceItem>();
        }

        public long ServiceItemId { get; set; }
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

        public virtual Service Service { get; set; } = null!;
        public virtual Tenant Tenant { get; set; } = null!;
        public virtual ICollection<UserServiceItem> UserServiceItems { get; set; }
    }
}
