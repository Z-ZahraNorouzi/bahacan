
using BusinessLogic.BusinessModelRules;
using BusinessModel;
using DataTransferModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Action
{
    public class UserPermissionAction : ActionBase
    {
        public UserPermissionAction()
        {
        }

        public UserPermissionAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<UserPermissionDataTransferModel>> GetAll(
            Expression<Func<UserPermissionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserPermissionDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<UserPermissionDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<UserPermissionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserPermissionDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.UserPermissionDao.GetAllCount(filter);
        }

        public async Task<UserPermissionBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserPermissionDao.GetByKey(input, includeProperties);
        }

        public void Add(UserPermissionBusinessModel input)
        {
            UserPermissionBusinessRule obj = new UserPermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserPermissionDao.Create(input);
        }

        public void Modify(UserPermissionBusinessModel input)
        {
            UserPermissionBusinessRule obj = new UserPermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserPermissionDao.Update(input);
        }

        public void Remove(UserPermissionBusinessModel input)
        {
            UserPermissionBusinessRule obj = new UserPermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserPermissionDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.UserPermissionDao.Delete(input);
        }

        public void Save(UserPermissionBusinessModel input)
        {
            if (input.UserPermissionId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.UserPermissionId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}
