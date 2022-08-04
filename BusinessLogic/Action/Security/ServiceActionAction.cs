
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
    public class ServiceActionAction : ActionBase
    {
        public ServiceActionAction()
        {
        }

        public ServiceActionAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<ServiceActionDataTransferModel>> GetAll(
            Expression<Func<ServiceActionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceActionDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<ServiceActionDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<ServiceActionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceActionDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.ServiceActionDao.GetAllCount(filter);
        }

        public async Task<ServiceActionBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceActionDao.GetByKey(input, includeProperties);
        }

        public void Add(ServiceActionBusinessModel input)
        {
            ServiceActionBusinessRule obj = new ServiceActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceActionDao.Create(input);
        }

        public void Modify(ServiceActionBusinessModel input)
        {
            ServiceActionBusinessRule obj = new ServiceActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceActionDao.Update(input);
        }

        public void Remove(ServiceActionBusinessModel input)
        {
            ServiceActionBusinessRule obj = new ServiceActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceActionDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.ServiceActionDao.Delete(input);
        }

        public void Save(ServiceActionBusinessModel input)
        {
            if (input.ServiceActionId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.ServiceActionId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}