using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class ServiceAction
    {
        public ServiceAction()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserPermissions = new HashSet<UserPermission>();
        }

        public long ServiceActionId { get; set; }
        public byte ActionId { get; set; }
        public int ServiceId { get; set; }
        public string? Title { get; set; }
        public string? ApiController { get; set; }
        public string? ApiService { get; set; }

        public virtual Action Action { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
