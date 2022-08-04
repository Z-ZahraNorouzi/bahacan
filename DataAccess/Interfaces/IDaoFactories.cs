using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDaoFactories
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        #region Security
        IActionDao ActionDao { get; }
        ILoginHistoryDao LoginHistoryDao { get; }
        ILoginStatusDao LoginStatusDao { get; }
        IRolePermissionDao RolePermissionDao { get; }
        IRoleDao RoleDao { get; }
        IServiceActionDao ServiceActionDao { get; }
        IServiceDao ServiceDao { get; }
        IServiceItemDao ServiceItemDao { get; }
        ITenantDao TenantDao { get; }
        IUserInfoDao UserInfoDao { get; }
        IUserInRoleDao UserInRoleDao { get; }
        IUserPermissionDao UserPermissionDao { get; }
        IUserServiceItemDao UserServiceItemDao { get; }
        #endregion
    }
}
