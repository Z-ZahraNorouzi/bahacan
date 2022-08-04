using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class RoleBusinessModel : BusinessModelBase
    {
        public int? RoleId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? DeletedById { get; set; }

        public virtual ICollection<RolePermissionBusinessModel>? RolePermissions { get; set; }
        public virtual ICollection<UserInRoleBusinessModel>? UserInRoles { get; set; }
    }
}
