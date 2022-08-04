
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
    public class RoleAction : ActionBase
    {
        public RoleAction()
        {
        }

        public RoleAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<RoleDataTransferModel>> GetAll(
            Expression<Func<RoleBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.RoleDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<RoleDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<RoleBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.RoleDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<RoleBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.RoleDao.GetAllCount(filter);
        }

        public async Task<RoleBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.RoleDao.GetByKey(input, includeProperties);
        }

        public void Add(RoleBusinessModel input)
        {
            RoleBusinessRule obj = new RoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RoleDao.Create(input);
        }

        public void Modify(RoleBusinessModel input)
        {
            RoleBusinessRule obj = new RoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RoleDao.Update(input);
        }

        public void Remove(RoleBusinessModel input)
        {
            RoleBusinessRule obj = new RoleBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RoleDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.RoleDao.Delete(input);
        }

        public void Save(RoleBusinessModel input)
        {
            if (input.RoleId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.RoleId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}

