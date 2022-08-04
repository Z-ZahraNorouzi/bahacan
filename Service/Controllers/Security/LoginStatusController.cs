
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/LoginStatus/[action]")]
    public class LoginStatusController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<LoginStatusBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<LoginStatusBusinessModel>();
            try
            {
                var action = new LoginStatusAction();
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
        public async Task<ResultModel<LoginStatusDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<LoginStatusDataTransferModel>();
            try
            {
                var action = new LoginStatusAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<LoginStatusBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<LoginStatusBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<LoginStatusDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<LoginStatusDataTransferModel>();

            try
            {
                var action = new LoginStatusAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<LoginStatusBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<LoginStatusBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<LoginStatusDataTransferModel>> GetAll()
        {
            var result = new ResultModel<LoginStatusDataTransferModel>();
            try
            {
                var action = new LoginStatusAction();
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
        public ResultModel<LoginStatusBusinessModel> Post([FromBody] LoginStatusBusinessModel input)
        {
            var result = new ResultModel<LoginStatusBusinessModel>();

            try
            {
                var action = new LoginStatusAction();
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
        public ResultModel<LoginStatusBusinessModel> Put(Int64 id, [FromBody] LoginStatusBusinessModel input)
        {
            var result = new ResultModel<LoginStatusBusinessModel>();

            try
            {
                var action = new LoginStatusAction();
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

                var action = new LoginStatusAction();
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
                var action = new LoginStatusAction();
                return await action.GetAllCount(GetFilterExpression<LoginStatusBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
