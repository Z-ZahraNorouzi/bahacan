
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/UserServiceItem/[action]")]
    public class UserServiceItemController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<UserServiceItemBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<UserServiceItemBusinessModel>();
            try
            {
                var action = new UserServiceItemAction();
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
        public async Task<ResultModel<UserServiceItemDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserServiceItemDataTransferModel>();
            try
            {
                var action = new UserServiceItemAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<UserServiceItemBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<UserServiceItemBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<UserServiceItemDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserServiceItemDataTransferModel>();

            try
            {
                var action = new UserServiceItemAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<UserServiceItemBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<UserServiceItemBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<UserServiceItemDataTransferModel>> GetAll()
        {
            var result = new ResultModel<UserServiceItemDataTransferModel>();
            try
            {
                var action = new UserServiceItemAction();
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
        public ResultModel<UserServiceItemBusinessModel> Post([FromBody] UserServiceItemBusinessModel input)
        {
            var result = new ResultModel<UserServiceItemBusinessModel>();

            try
            {
                var action = new UserServiceItemAction();
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
        public ResultModel<UserServiceItemBusinessModel> Put(Int64 id, [FromBody] UserServiceItemBusinessModel input)
        {
            var result = new ResultModel<UserServiceItemBusinessModel>();

            try
            {
                var action = new UserServiceItemAction();
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

                var action = new UserServiceItemAction();
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
                var action = new UserServiceItemAction();
                return await action.GetAllCount(GetFilterExpression<UserServiceItemBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}