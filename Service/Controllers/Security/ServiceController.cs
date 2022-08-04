
using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/Service/[action]")]
    public class ServiceController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ResultModel<ServiceBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<ServiceBusinessModel>();
            try
            {
                var action = new ServiceAction();
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
        public async Task<ResultModel<ServiceDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceDataTransferModel>();
            try
            {
                var action = new ServiceAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<ServiceBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<ServiceBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<ServiceDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<ServiceDataTransferModel>();

            try
            {
                var action = new ServiceAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<ServiceBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<ServiceBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<ServiceDataTransferModel>> GetAll()
        {
            var result = new ResultModel<ServiceDataTransferModel>();
            try
            {
                var action = new ServiceAction();
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
        public ResultModel<ServiceBusinessModel> Post([FromBody] ServiceBusinessModel input)
        {
            var result = new ResultModel<ServiceBusinessModel>();

            try
            {
                var action = new ServiceAction();
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
        public ResultModel<ServiceBusinessModel> Put(Int64 id, [FromBody] ServiceBusinessModel input)
        {
            var result = new ResultModel<ServiceBusinessModel>();

            try
            {
                var action = new ServiceAction();
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

                var action = new ServiceAction();
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
                var action = new ServiceAction();
                return await action.GetAllCount(GetFilterExpression<ServiceBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}
