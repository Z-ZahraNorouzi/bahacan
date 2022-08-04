
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
    public class LoginHistoryAction : ActionBase
    {
        public LoginHistoryAction()
        {
        }

        public LoginHistoryAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<LoginHistoryDataTransferModel>> GetAll(
            Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginHistoryDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<LoginHistoryDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginHistoryDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.LoginHistoryDao.GetAllCount(filter);
        }

        public async Task<LoginHistoryBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginHistoryDao.GetByKey(input, includeProperties);
        }

        public void Add(LoginHistoryBusinessModel input)
        {
            LoginHistoryBusinessRule obj = new LoginHistoryBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginHistoryDao.Create(input);
        }

        public void Modify(LoginHistoryBusinessModel input)
        {
            LoginHistoryBusinessRule obj = new LoginHistoryBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginHistoryDao.Update(input);
        }

        public void Remove(LoginHistoryBusinessModel input)
        {
            LoginHistoryBusinessRule obj = new LoginHistoryBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginHistoryDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.LoginHistoryDao.Delete(input);
        }

        public void Save(LoginHistoryBusinessModel input)
        {
            if (input.LoginHistoryId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.LoginHistoryId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}
