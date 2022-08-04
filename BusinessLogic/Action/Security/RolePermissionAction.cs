
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
    public class RolePermissionAction : ActionBase
    {
        public RolePermissionAction()
        {
        }

        public RolePermissionAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<RolePermissionDataTransferModel>> GetAll(
            Expression<Func<RolePermissionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.RolePermissionDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<RolePermissionDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<RolePermissionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.RolePermissionDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.RolePermissionDao.GetAllCount(filter);
        }

        public async Task<RolePermissionBusinessModel?> Get(Int64 input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.RolePermissionDao.GetByKey(input, includeProperties);
        }

        public void Add(RolePermissionBusinessModel input)
        {
            RolePermissionBusinessRule obj = new RolePermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RolePermissionDao.Create(input);
        }

        public void Modify(RolePermissionBusinessModel input)
        {
            RolePermissionBusinessRule obj = new RolePermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RolePermissionDao.Update(input);
        }

        public void Remove(RolePermissionBusinessModel input)
        {
            RolePermissionBusinessRule obj = new RolePermissionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.RolePermissionDao.Delete(input);
        }

        public void Remove(Int64 input)
        {
            FactoryContainer.Factory.RolePermissionDao.Delete(input);
        }

        public void Save(RolePermissionBusinessModel input)
        {
            if (input.RolePermissionId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.RolePermissionId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}