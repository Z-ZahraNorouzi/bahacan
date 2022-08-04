
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/ServiceAction/[action]")]
    public class ServiceActionController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<ServiceActionBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<ServiceActionBusinessModel>();
            try
            {
                var action = new ServiceActionAction();
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
        public async Task<ResultModel<ServiceActionDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceActionDataTransferModel>();
            try
            {
                var action = new ServiceActionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<ServiceActionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<ServiceActionBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<ServiceActionDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceActionDataTransferModel>();

            try
            {
                var action = new ServiceActionAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<ServiceActionBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<ServiceActionBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<ServiceActionDataTransferModel>> GetAll()
        {
            var result = new ResultModel<ServiceActionDataTransferModel>();
            try
            {
                var action = new ServiceActionAction();
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
        public ResultModel<ServiceActionBusinessModel> Post([FromBody] ServiceActionBusinessModel input)
        {
            var result = new ResultModel<ServiceActionBusinessModel>();

            try
            {
                var action = new ServiceActionAction();
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
        public ResultModel<ServiceActionBusinessModel> Put(Int64 id, [FromBody] ServiceActionBusinessModel input)
        {
            var result = new ResultModel<ServiceActionBusinessModel>();

            try
            {
                var action = new ServiceActionAction();
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

                var action = new ServiceActionAction();
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
                var action = new ServiceActionAction();
                return await action.GetAllCount(GetFilterExpression<ServiceActionBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
