using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class Tenant
    {
        public Tenant()
        {
            LoginHistories = new HashSet<LoginHistory>();
            RolePermissions = new HashSet<RolePermission>();
            ServiceItems = new HashSet<ServiceItem>();
            UserInRoles = new HashSet<UserInRole>();
            UserPermissions = new HashSet<UserPermission>();
            UserTenants = new HashSet<UserTenant>();
        }

        public long TenantId { get; set; }
        public string? Title { get; set; }
        public long? CompanyId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreateById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? DeletedById { get; set; }

        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserTenant> UserTenants { get; set; }
    }
}
