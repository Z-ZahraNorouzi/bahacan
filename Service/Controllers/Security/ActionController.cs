using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/Action/[action]")]
    public class ActionController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<ActionBusinessModel>> Get(Int16 id)
        {
            var result = new ResultModel<ActionBusinessModel>();
            try
            {
                var action = new ActionAction();
                var data = await action.Get((byte)id);
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
        public async Task<ResultModel<ActionDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ActionDataTransferModel>();
            try
            {
                var action = new ActionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<ActionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<ActionBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<ActionDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ActionDataTransferModel>();

            try
            {
                var action = new ActionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<ActionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<ActionBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<ActionDataTransferModel>> GetAll()
        {
            var result = new ResultModel<ActionDataTransferModel>();
            try
            {
                var action = new ActionAction();
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
        public ResultModel<ActionBusinessModel> Post([FromBody] ActionBusinessModel input)
        {
            var result = new ResultModel<ActionBusinessModel>();

            try
            {
                var action = new ActionAction();
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
        public ResultModel<ActionBusinessModel> Put(Int16 id, [FromBody] ActionBusinessModel input)
        {
            var result = new ResultModel<ActionBusinessModel>();

            try
            {
                var action = new ActionAction();
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
        public void Delete(Int16 id)
        {
            try
            {

                var action = new ActionAction();
                action.Remove((byte)id);
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
                var action = new ActionAction();
                return await action.GetAllCount(GetFilterExpression<ActionBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}