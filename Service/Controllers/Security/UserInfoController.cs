using BusinessLogic.Action;
using Common.DTO;
using ViewModel;
using Microsoft.AspNetCore.Mvc;
using BusinessModel;
using DataTransferModel.Security;
using Microsoft.AspNetCore.Authorization;
using Service.Security;

namespace Service.Controllers
{
    [Route("api/UserInfo/[action]")]
    public class UserInfoController : BaseApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

            public UserInfoController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        [HttpGet("{id}")]
        public async Task<ResultModel<UserInfoBusinessModel>> Get(Int64 id)
        {
            var result = new ResultModel<UserInfoBusinessModel>();
            try
            {
                var action = new UserInfoAction();
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
        public async Task<ResultModel<UserInfoDataTransferModel>> Get([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserInfoDataTransferModel>();
            try
            {
                var action = new UserInfoAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetFilterExpression<UserInfoBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetFilterExpression<UserInfoBusinessModel>(model.Filters), model.OrderBy);

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
        public async Task<ResultModel<UserInfoDataTransferModel>> Suggestion([FromBody] DataFilterWithPaging model)
        {
            var result = new ResultModel<UserInfoDataTransferModel>();

            try
            {
                var action = new UserInfoAction();

                result.Items = HasPaging(model)
                    ? await action.GetAll(model.PageNumber, model.PageSize, GetSuggestionExpression<UserInfoBusinessModel>(model.Filters), model.OrderBy)
                    : await action.GetAll(GetSuggestionExpression<UserInfoBusinessModel>(model.Filters), model.OrderBy);
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
        public async Task<ResultModel<UserInfoDataTransferModel>> GetAll()
        {
            var result = new ResultModel<UserInfoDataTransferModel>();
            try
            {
                var action = new UserInfoAction();
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
        public ResultModel<UserInfoBusinessModel> Post([FromBody] UserInfoBusinessModel input)
        {
            var result = new ResultModel<UserInfoBusinessModel>();

            try
            {
                var action = new UserInfoAction();
                input.Password = MD5Security.GetMd5Hash(input.Password);
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
        public ResultModel<UserInfoBusinessModel> Put(Int64 id, [FromBody] UserInfoBusinessModel input)
        {
            var result = new ResultModel<UserInfoBusinessModel>();

            try
            {
                var action = new UserInfoAction();
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

                var action = new UserInfoAction();
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
                var action = new UserInfoAction();
                return await action.GetAllCount(GetFilterExpression<UserInfoBusinessModel>(input.Filters));
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
        
        [HttpGet]
        public async Task<UserInfoBusinessModel> MobileLogin(string mobileNumber, string activeCode)
        {
            try
            {
                var result = new UserInfoBusinessModel();
                var action = new UserInfoAction();
                var userInfo = await action.GetAll(x => x.MobileNo == mobileNumber && x.ActivationCode == activeCode && x.ActivationCode != "");
                if (userInfo != null && userInfo.Count > 0)
                {
                    var user = await action.Get(userInfo.FirstOrDefault().UserInfoId.Value);
                    user.ActivationCode = string.Empty;
                    action.Modify(user);
                    result = user;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }

        [HttpGet]
        public async Task<UserInfoBusinessModel> UserLogin(string userName, string password)
        {
            try
            {
                var result = new UserInfoBusinessModel();
                var action = new UserInfoAction();
                var userInfo = await action.GetAll(x => x.UserName == userName && x.Password == Security.MD5Security.GetMd5Hash(password));
                if (userInfo != null && userInfo.Count > 0)
                {
                    var user = await action.Get(userInfo.FirstOrDefault().UserInfoId.Value);
                    user.ActivationCode = string.Empty;
                    action.Modify(user);
                    result = user;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
    }
}