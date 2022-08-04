
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/LoginHistory/[action]")]
    public class LoginHistoryController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<LoginHistoryBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<LoginHistoryBusinessModel>();
            try
            {
                var action = new LoginHistoryAction();
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
        public async Task<ResultModel<LoginHistoryDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<LoginHistoryDataTransferModel>();
            try
            {
                var action = new LoginHistoryAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<LoginHistoryBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<LoginHistoryBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<LoginHistoryDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<LoginHistoryDataTransferModel>();

            try
            {
                var action = new LoginHistoryAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<LoginHistoryBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<LoginHistoryBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<LoginHistoryDataTransferModel>> GetAll()
        {
            var result = new ResultModel<LoginHistoryDataTransferModel>();
            try
            {
                var action = new LoginHistoryAction();
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
        public ResultModel<LoginHistoryBusinessModel> Post([FromBody] LoginHistoryBusinessModel input)
        {
            var result = new ResultModel<LoginHistoryBusinessModel>();

            try
            {
                var action = new LoginHistoryAction();
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
        public ResultModel<LoginHistoryBusinessModel> Put(Int64 id, [FromBody] LoginHistoryBusinessModel input)
        {
            var result = new ResultModel<LoginHistoryBusinessModel>();

            try
            {
                var action = new LoginHistoryAction();
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

                var action = new LoginHistoryAction();
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
                var action = new LoginHistoryAction();
                return await action.GetAllCount(GetFilterExpression<LoginHistoryBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}