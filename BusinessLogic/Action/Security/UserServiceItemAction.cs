
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
    public class UserServiceItemAction : ActionBase
    {
        public UserServiceItemAction()
        {
        }

        public UserServiceItemAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<UserServiceItemDataTransferModel>> GetAll(
            Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserServiceItemDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<UserServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserServiceItemDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.UserServiceItemDao.GetAllCount(filter);
        }

        public async Task<UserServiceItemBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.UserServiceItemDao.GetByKey(input, includeProperties);
        }

        public void Add(UserServiceItemBusinessModel input)
        {
            UserServiceItemBusinessRule obj = new UserServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserServiceItemDao.Create(input);
        }

        public void Modify(UserServiceItemBusinessModel input)
        {
            UserServiceItemBusinessRule obj = new UserServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserServiceItemDao.Update(input);
        }

        public void Remove(UserServiceItemBusinessModel input)
        {
            UserServiceItemBusinessRule obj = new UserServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.UserServiceItemDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.UserServiceItemDao.Delete(input);
        }

        public void Save(UserServiceItemBusinessModel input)
        {
            if (input.UserServiceItemId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.UserServiceItemId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}