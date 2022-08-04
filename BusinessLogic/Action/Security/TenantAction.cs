
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
    public class TenantAction : ActionBase
    {
        public TenantAction()
        {
        }

        public TenantAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<TenantDataTransferModel>> GetAll(
            Expression<Func<TenantBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.TenantDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<TenantDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<TenantBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.TenantDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<TenantBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.TenantDao.GetAllCount(filter);
        }

        public async Task<TenantBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.TenantDao.GetByKey(input, includeProperties);
        }

        public void Add(TenantBusinessModel input)
        {
            TenantBusinessRule obj = new TenantBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.TenantDao.Create(input);
        }

        public void Modify(TenantBusinessModel input)
        {
            TenantBusinessRule obj = new TenantBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.TenantDao.Update(input);
        }

        public void Remove(TenantBusinessModel input)
        {
            TenantBusinessRule obj = new TenantBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.TenantDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.TenantDao.Delete(input);
        }

        public void Save(TenantBusinessModel input)
        {
            if (input.TenantId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.TenantId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}