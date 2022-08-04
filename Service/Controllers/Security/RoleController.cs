
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/Role/[action]")]
    public class RoleController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<RoleBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<RoleBusinessModel>();
            try
            {
                var action = new RoleAction();
                var data = await action.Get(id);
                result.Success = true;
                result.Result = data;
                result.TotalCount = 1;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
                result.TotalCount = 0;
            }
            return result;
        }

        [HttpPost]
        public async Task<ResultModel<RoleDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<RoleDataTransferModel>();
            try
            {
                var action = new RoleAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<RoleBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<RoleBusinessModel>(model.Filters), model.OrderBy);

                result.TotalCount = await Count(model);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.TotalCount = 0;
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;

        }
        [HttpPost]
        public async Task<ResultModel<RoleDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<RoleDataTransferModel>();

            try
            {
                var action = new RoleAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<RoleBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<RoleBusinessModel>(model.Filters), model.OrderBy);
                result.Success = true;
                result.TotalCount = await Count(model);
            }
            catch (Exception ex)
            {
                result.TotalCount = 0;
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;

        }
        [HttpGet]
        public async Task<ResultModel<RoleDataTransferModel>> GetAll()
        {
            var result = new ResultModel<RoleDataTransferModel>();
            try
            {
                var action = new RoleAction();
                result.Items = await action.GetAll(null, "");
                result.TotalCount = result.Items.Count;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.TotalCount = 0;
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;

        }


        [HttpPost]
        public ResultModel<RoleBusinessModel> Post([FromBody] RoleBusinessModel input)
        {
            var result = new ResultModel<RoleBusinessModel>();

            try
            {
                var action = new RoleAction();
                action.Add(input);
                result.Success = true;
                result.TotalCount = 1;
                result.Result = input;
            }
            catch (Exception ex)
            {
                result.TotalCount = 0;
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;
        }

        [HttpPut("{id}")]
        public ResultModel<RoleBusinessModel> Put(Int64 id, [FromBody] RoleBusinessModel input)
        {
            var result = new ResultModel<RoleBusinessModel>();

            try
            {
                var action = new RoleAction();
                action.Modify(input);
                result.Success = true;
                result.TotalCount = 1;
                result.Result = input;
            }
            catch (Exception ex)
            {
                result.TotalCount = 0;
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;

        }

        [HttpDelete("{id}")]
        public void Delete(Int64 id)
        {
            try
            {

                var action = new RoleAction();
                action.Remove(id);
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }

        [HttpPost]
        private async Task<long> Count([FromBody] DataFilterWithPaging input)
        {
            try
            {
                var action = new RoleAction();
                return await action.GetAllCount(GetFilterExpression<RoleBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
