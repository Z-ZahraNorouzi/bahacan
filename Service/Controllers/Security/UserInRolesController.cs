
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/UserInRole/[action]")]
    public class UserInRoleController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<UserInRoleBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<UserInRoleBusinessModel>();
            try
            {
                var action = new UserInRoleAction();
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
        public async Task<ResultModel<UserInRoleDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserInRoleDataTransferModel>();
            try
            {
                var action = new UserInRoleAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<UserInRoleBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<UserInRoleBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<UserInRoleDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserInRoleDataTransferModel>();

            try
            {
                var action = new UserInRoleAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<UserInRoleBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<UserInRoleBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<UserInRoleDataTransferModel>> GetAll()
        {
            var result = new ResultModel<UserInRoleDataTransferModel>();
            try
            {
                var action = new UserInRoleAction();
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
        public ResultModel<UserInRoleBusinessModel> Post([FromBody] UserInRoleBusinessModel input)
        {
            var result = new ResultModel<UserInRoleBusinessModel>();

            try
            {
                var action = new UserInRoleAction();
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
        public ResultModel<UserInRoleBusinessModel> Put(Int64 id, [FromBody] UserInRoleBusinessModel input)
        {
            var result = new ResultModel<UserInRoleBusinessModel>();

            try
            {
                var action = new UserInRoleAction();
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

                var action = new UserInRoleAction();
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
                var action = new UserInRoleAction();
                return await action.GetAllCount(GetFilterExpression<UserInRoleBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
