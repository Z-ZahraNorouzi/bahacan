using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            LoginHistories = new HashSet<LoginHistory>();
            UserInRoles = new HashSet<UserInRole>();
            UserPermissions = new HashSet<UserPermission>();
            UserServiceItems = new HashSet<UserServiceItem>();
            UserTenants = new HashSet<UserTenant>();
        }

        public long UserInfoId { get; set; }
        public string? UserName { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public long AvatarImageId { get; set; }
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

        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserServiceItem> UserServiceItems { get; set; }
        public virtual ICollection<UserTenant> UserTenants { get; set; }
    }
}
