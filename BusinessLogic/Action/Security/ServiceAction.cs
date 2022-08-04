
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
    public class ServiceAction : ActionBase
    {
        public ServiceAction()
        {
        }

        public ServiceAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<ServiceDataTransferModel>> GetAll(
            Expression<Func<ServiceBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<ServiceDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<ServiceBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<ServiceBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.ServiceDao.GetAllCount(filter);
        }

        public async Task<ServiceBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.ServiceDao.GetByKey(input, includeProperties);
        }

        public async Task<IList<ServiceDataTransferModel>> GetByUserId(Int64 input)
        {
            return await FactoryContainer.Factory.ServiceDao.GetByUserId(input);
        }

        public void Add(ServiceBusinessModel input)
        {
            ServiceBusinessRule obj = new ServiceBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceDao.Create(input);
        }

        public void Modify(ServiceBusinessModel input)
        {
            ServiceBusinessRule obj = new ServiceBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceDao.Update(input);
        }

        public void Remove(ServiceBusinessModel input)
        {
            ServiceBusinessRule obj = new ServiceBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ServiceDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.ServiceDao.Delete(input);
        }

        public void Save(ServiceBusinessModel input)
        {
            if (input.ServiceId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.ServiceId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}