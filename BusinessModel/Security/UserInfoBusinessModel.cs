using DataTransferModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class UserInfoBusinessModel : BusinessModelBase
    {

        public UserInfoBusinessModel()
        {
            LoginHistories = new HashSet<LoginHistoryBusinessModel>();
            UserInRoles = new HashSet<UserInRoleBusinessModel>();
            UserPermissions = new HashSet<UserPermissionBusinessModel>();
            UserServiceItems = new HashSet<UserServiceItemBusinessModel>();
            UserTenants = new HashSet<UserTenantBusinessModel>();
        }

        public long? UserInfoId { get; set; }
        public string? UserName { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public long? AvatarImageId { get; set; }
        public byte Status { get; set; }
        public string? ActivationCode { get; set; }
        public long? RealPersonId { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual LoginHistoryBusinessModel? CurrentLogin { get; set; }
        public virtual ICollection<LoginHistoryBusinessModel>? LoginHistories { get; set; }
        public virtual ICollection<UserInRoleBusinessModel>? UserInRoles { get; set; }
        public virtual ICollection<UserPermissionBusinessModel>? UserPermissions { get; set; }
        public virtual ICollection<UserServiceItemBusinessModel>? UserServiceItems { get; set; }
        public virtual ICollection<ServiceDataTransferModel>? Services { get; set; }
        public virtual ICollection<UserTenantBusinessModel> UserTenants { get; set; }

    }
}
