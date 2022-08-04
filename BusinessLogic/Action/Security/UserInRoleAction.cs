
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
    public class UserInRoleAction : ActionBase
    {
        public UserInRoleAction()
        {
        }

        public UserInRoleAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<UserInRoleDataTransferModel>> GetAll(
            Expression<Func<UserInRoleBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserInRoleDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<UserInRoleDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<UserInRoleBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserInRoleDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.UserInRoleDao.GetAllCount(filter);
        }

        public async Task<UserInRoleBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserInRoleDao.GetByKey(input, includeProperties);
        }

        public void Add(UserInRoleBusinessModel input)
        {
            UserInRoleBusinessRule obj = new UserInRoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserInRoleDao.Create(input);
        }

        public void Modify(UserInRoleBusinessModel input)
        {
            UserInRoleBusinessRule obj = new UserInRoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserInRoleDao.Update(input);
        }

        public void Remove(UserInRoleBusinessModel input)
        {
            UserInRoleBusinessRule obj = new UserInRoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserInRoleDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.UserInRoleDao.Delete(input);
        }

        public void Save(UserInRoleBusinessModel input)
        {
            if (input.UserInRoleId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.UserInRoleId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}
