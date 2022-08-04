
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/UserPermission/[action]")]
    public class UserPermissionController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<UserPermissionBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<UserPermissionBusinessModel>();
            try
            {
                var action = new UserPermissionAction();
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
        public async Task<ResultModel<UserPermissionDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserPermissionDataTransferModel>();
            try
            {
                var action = new UserPermissionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<UserPermissionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<UserPermissionBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<UserPermissionDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserPermissionDataTransferModel>();

            try
            {
                var action = new UserPermissionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<UserPermissionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<UserPermissionBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<UserPermissionDataTransferModel>> GetAll()
        {
            var result = new ResultModel<UserPermissionDataTransferModel>();
            try
            {
                var action = new UserPermissionAction();
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
        public ResultModel<UserPermissionBusinessModel> Post([FromBody] UserPermissionBusinessModel input)
        {
            var result = new ResultModel<UserPermissionBusinessModel>();

            try
            {
                var action = new UserPermissionAction();
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
        public ResultModel<UserPermissionBusinessModel> Put(Int64 id, [FromBody] UserPermissionBusinessModel input)
        {
            var result = new ResultModel<UserPermissionBusinessModel>();

            try
            {
                var action = new UserPermissionAction();
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

                var action = new UserPermissionAction();
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
                var action = new UserPermissionAction();
                return await action.GetAllCount(GetFilterExpression<UserPermissionBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
