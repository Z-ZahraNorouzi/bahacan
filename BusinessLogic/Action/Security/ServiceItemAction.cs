
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
    public class ServiceItemAction : ActionBase
    {
        public ServiceItemAction()
        {
        }

        public ServiceItemAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<ServiceItemDataTransferModel>> GetAll(
            Expression<Func<ServiceItemBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceItemDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<ServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<ServiceItemBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceItemDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.ServiceItemDao.GetAllCount(filter);
        }

        public async Task<ServiceItemBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceItemDao.GetByKey(input, includeProperties);
        }

        public void Add(ServiceItemBusinessModel input)
        {
            ServiceItemBusinessRule obj = new ServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceItemDao.Create(input);
        }

        public void Modify(ServiceItemBusinessModel input)
        {
            ServiceItemBusinessRule obj = new ServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceItemDao.Update(input);
        }

        public void Remove(ServiceItemBusinessModel input)
        {
            ServiceItemBusinessRule obj = new ServiceItemBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceItemDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.ServiceItemDao.Delete(input);
        }

        public void Save(ServiceItemBusinessModel input)
        {
            if (input.ServiceItemId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.ServiceItemId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}