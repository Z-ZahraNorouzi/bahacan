
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/ServiceItem/[action]")]
    public class ServiceItemController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<ServiceItemBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<ServiceItemBusinessModel>();
            try
            {
                var action = new ServiceItemAction();
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
        public async Task<ResultModel<ServiceItemDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceItemDataTransferModel>();
            try
            {
                var action = new ServiceItemAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<ServiceItemBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<ServiceItemBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<ServiceItemDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceItemDataTransferModel>();

            try
            {
                var action = new ServiceItemAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<ServiceItemBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<ServiceItemBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<ServiceItemDataTransferModel>> GetAll()
        {
            var result = new ResultModel<ServiceItemDataTransferModel>();
            try
            {
                var action = new ServiceItemAction();
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
        public ResultModel<ServiceItemBusinessModel> Post([FromBody] ServiceItemBusinessModel input)
        {
            var result = new ResultModel<ServiceItemBusinessModel>();

            try
            {
                var action = new ServiceItemAction();
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
        public ResultModel<ServiceItemBusinessModel> Put(Int64 id, [FromBody] ServiceItemBusinessModel input)
        {
            var result = new ResultModel<ServiceItemBusinessModel>();

            try
            {
                var action = new ServiceItemAction();
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

                var action = new ServiceItemAction();
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
                var action = new ServiceItemAction();
                return await action.GetAllCount(GetFilterExpression<ServiceItemBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
