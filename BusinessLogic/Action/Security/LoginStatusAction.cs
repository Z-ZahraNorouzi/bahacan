
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
    public class LoginStatusAction : ActionBase
    {
        public LoginStatusAction()
        {
        }

        public LoginStatusAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<LoginStatusDataTransferModel>> GetAll(
            Expression<Func<LoginStatusBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginStatusDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<LoginStatusDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<LoginStatusBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginStatusDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.LoginStatusDao.GetAllCount(filter);
        }

        public async Task<LoginStatusBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.LoginStatusDao.GetByKey(input, includeProperties);
        }

        public void Add(LoginStatusBusinessModel input)
        {
            LoginStatusBusinessRule obj = new LoginStatusBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginStatusDao.Create(input);
        }

        public void Modify(LoginStatusBusinessModel input)
        {
            LoginStatusBusinessRule obj = new LoginStatusBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginStatusDao.Update(input);
        }

        public void Remove(LoginStatusBusinessModel input)
        {
            LoginStatusBusinessRule obj = new LoginStatusBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.LoginStatusDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.LoginStatusDao.Delete(input);
        }

        public void Save(LoginStatusBusinessModel input)
        {
            if (input.LoginStatusId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.LoginStatusId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}