using DataAccess.Interfaces;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityFramework.Model;

namespace DataAccess.EntityFramework.MS_SQL
{
    public class DaoFactory : IDaoFactories
    {
        private Stack<IDbContextTransaction>? _transactions;

        private Stack<IDbContextTransaction> Transactions
        {
            get
            {
                if (_transactions == null)
                    _transactions = new Stack<IDbContextTransaction>();
                return _transactions;
            }
        }

        private DbContext DatabaseContext { get; set; }

        public DaoFactory()
        {
            ReNewDbContext();
        }

        public void BeginTransaction()
        {
            Transactions.Push(DatabaseContext.Database.BeginTransaction());
        }

        public void CommitTransaction()
        {
            if (Transactions.Count == 0)
                return;

            IDbContextTransaction txn = Transactions.Pop();
            txn.Commit();
        }

        public void RollbackTransaction()
        {
            if (Transactions.Count == 0)
                return;

            IDbContextTransaction txn = Transactions.Pop();
            try
            {
                txn.Rollback();
            }
            finally
            {
                ReNewDbContext();
            }
        }


        private void ReNewDbContext()
        {
            DatabaseContext = null;
            DatabaseContext = new TrPosContext();
        }

        #region Security
        public IActionDao ActionDao { get { return new ActionDao(DatabaseContext); } }

        public ILoginHistoryDao LoginHistoryDao { get { return new LoginHistoryDao(DatabaseContext); } }

        public ILoginStatusDao LoginStatusDao { get { return new LoginStatusDao(DatabaseContext); } }

        public IRolePermissionDao RolePermissionDao { get { return new RolePermissionDao(DatabaseContext); } }

        public IRoleDao RoleDao { get { return new RoleDao(DatabaseContext); } }

        public IServiceActionDao ServiceActionDao { get { return new ServiceActionDao(DatabaseContext); } }

        public IServiceDao ServiceDao { get { return new ServiceDao(DatabaseContext); } }

        public IServiceItemDao ServiceItemDao { get { return new ServiceItemDao(DatabaseContext); } }

        public ITenantDao TenantDao { get { return new TenantDao(DatabaseContext); } }

        public IUserInfoDao UserInfoDao { get { return new UserInfoDao(DatabaseContext); } }

        public IUserInRoleDao UserInRoleDao { get { return new UserInRoleDao(DatabaseContext); } }

        public IUserPermissionDao UserPermissionDao { get { return new UserPermissionDao(DatabaseContext); } }

        public IUserServiceItemDao UserServiceItemDao { get { return new UserServiceItemDao(DatabaseContext); } }

        #endregion

    }
}
