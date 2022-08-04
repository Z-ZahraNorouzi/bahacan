
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/RolePermission/[action]")]
    public class RolePermissionController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<RolePermissionBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<RolePermissionBusinessModel>();
            try
            {
                var action = new RolePermissionAction();
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
        public async Task<ResultModel<RolePermissionDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<RolePermissionDataTransferModel>();
            try
            {
                var action = new RolePermissionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<RolePermissionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<RolePermissionBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<RolePermissionDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<RolePermissionDataTransferModel>();

            try
            {
                var action = new RolePermissionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<RolePermissionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<RolePermissionBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<RolePermissionDataTransferModel>> GetAll()
        {
            var result = new ResultModel<RolePermissionDataTransferModel>();
            try
            {
                var action = new RolePermissionAction();
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
        public ResultModel<RolePermissionBusinessModel> Post([FromBody] RolePermissionBusinessModel input)
        {
            var result = new ResultModel<RolePermissionBusinessModel>();

            try
            {
                var action = new RolePermissionAction();
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
        public ResultModel<RolePermissionBusinessModel> Put(Int64 id, [FromBody] RolePermissionBusinessModel input)
        {
            var result = new ResultModel<RolePermissionBusinessModel>();

            try
            {
                var action = new RolePermissionAction();
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

                var action = new RolePermissionAction();
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
                var action = new RolePermissionAction();
                return await action.GetAllCount(GetFilterExpression<RolePermissionBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
