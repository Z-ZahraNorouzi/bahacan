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
    public class ActionAction : ActionBase
    {
        public ActionAction()
        {
        }

        public ActionAction(FactoryContainer factoryContainer)
        {
            FactoryContainer = factoryContainer;
        }

        public async Task<IList<ActionDataTransferModel>> GetAll(
            Expression<Func<ActionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ActionDao.GetAll(filter, orderBy, includeProperties);
        }

        public async Task<IList<ActionDataTransferModel>> GetAll(int pageNumber, int pageSize,
            Expression<Func<ActionBusinessModel, bool>>? filter = null,
            string orderBy = "", string includeProperties = "")
        {
            return await FactoryContainer.Factory.ActionDao.GetAll(pageNumber, pageSize, filter, orderBy, includeProperties
                );
        }

        public async Task<Int64> GetAllCount(Expression<Func<ActionBusinessModel, bool>>? filter = null)
        {
            return await FactoryContainer.Factory.ActionDao.GetAllCount(filter);
        }

        public async Task<ActionBusinessModel?> Get(byte input, string includeProperties = "")
        {
            return await FactoryContainer.Factory.ActionDao.GetByKey(input, includeProperties);
        }

        public void Add(ActionBusinessModel input)
        {
            ActionBusinessRule obj = new ActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Add))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ActionDao.Create(input);
        }

        public void Modify(ActionBusinessModel input)
        {
            ActionBusinessRule obj = new ActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Modify))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ActionDao.Update(input);
        }

        public void Remove(ActionBusinessModel input)
        {
            ActionBusinessRule obj = new ActionBusinessRule(input);
            if (!obj.Validate(BusinessRules.BusinessObjectState.Remove))
                throw new Exception(obj.BrokenRules.ToString());

            FactoryContainer.Factory.ActionDao.Delete(input);
        }

        public void Remove(byte input)
        {
            FactoryContainer.Factory.ActionDao.Delete(input);
        }

        public void Save(ActionBusinessModel input)
        {
            if (input.ActionId.HasValue == false)
            {
                Add(input);
                return;
            }

            var result = Get(input.ActionId.Value);
            if (result != null)
                Modify(input);
            else
                Add(input);
        }
    }
}
