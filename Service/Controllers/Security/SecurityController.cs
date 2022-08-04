using BusinessLogic.Action;
using BusinessModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Security.JWTService;
using Resources;
using Configs;
using ExternalServices;
using DataTransferModel.Security;

namespace Service.Controllers
{
    [Route("api/Security/[action]")]
    public class SecurityController : BaseApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        //private JwtService jwtService;
        public SecurityController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        [HttpPost, HttpOptions]
        public async Task<ResultModel<UserInfoBusinessModel>> MobileLogin([FromBody] MobileLoginViewModel input)
        {
            var result = new ResultModel<UserInfoBusinessModel>();
            result.Success = false;
            result.TotalCount = 0;
            var loginHistory = new LoginHistoryBusinessModel();
            loginHistory.TenantId = input.TenantId;
            try
            {
                var userAction = new UserInfoAction();
                var userController = new UserInfoController(_httpContextAccessor);
                var user = await userController.MobileLogin(input.MobileNumber, input.ActiveCode);
                if (user.UserInfoId == null)
                {
                    result.Error = Errors.UsernameOrPasswordIsInvalid;
                    return result;
                }

                user = await userAction.Get(user.UserInfoId.Value);
                loginHistory.UserId = user.UserInfoId.Value;
                loginHistory.Token = Guid.NewGuid();
                loginHistory.LoginDateTime = DateTime.Now;
                loginHistory.ExpireDateTime = DateTime.Now.AddHours(16);
                if (user == null)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_IncorrectPassword;
                    result.Error = Errors.UsernameOrPasswordIsInvalid;
                    return result;
                }

                if (!user.IsActive)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_InactiveUser;
                    result.Error = Errors.UserIsInActive;
                    return result;
                }
                if (user.IsLock)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_LockUser;
                    result.Error = Errors.UserIsLocked;
                    return result;
                }
                loginHistory.LoginStatusId = 1;

                user.CurrentLogin = loginHistory;
                result.Result = user;
                result.TotalCount = 1;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            finally
            {
                PostLoginHistory(loginHistory);
            }
            return result;
        }


        [HttpPost, HttpOptions]
        public async Task<ResultModel<UserInfoBusinessModel>> UserLogin([FromBody] UserNameLoginViewModel input)
        {
            var result = new ResultModel<UserInfoBusinessModel>();
            result.Success = false;
            result.TotalCount = 0;
            var loginHistory = new LoginHistoryBusinessModel();
            try
            {
                var userAction = new UserInfoAction();
                var userController = new UserInfoController(_httpContextAccessor);
                var user = await userController.UserLogin(input.UserName, input.Password);
                if (user.UserInfoId == null)
                {
                    result.Error = Errors.UsernameOrPasswordIsInvalid;
                    return result;
                }

                user = await userAction.Get(user.UserInfoId.Value);
                loginHistory.UserId = user.UserInfoId.Value;
                loginHistory.Token = Guid.NewGuid();
                loginHistory.LoginDateTime = DateTime.Now;
                loginHistory.ExpireDateTime = DateTime.Now.AddHours(16);
                loginHistory.TenantId = input.TenantId;
                if (user == null)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_IncorrectPassword;
                    result.Error = Errors.UsernameOrPasswordIsInvalid;
                    return result;
                }

                if (!user.IsActive)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_InactiveUser;
                    result.Error = Errors.UserIsInActive;
                    return result;
                }
                if (user.IsLock)
                {
                    loginHistory.LoginStatusId = Config.LoginStatus_LockUser;
                    result.Error = Errors.UserIsLocked;
                    return result;
                }
                loginHistory.LoginStatusId = 1;

                user.CurrentLogin = loginHistory;
                result.Result = user;
                result.TotalCount = 1;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            finally
            {
                PostLoginHistory(loginHistory);
            }
            return result;
        }



        [HttpPost, HttpOptions]
        public async Task<ResultModel<TenantDataTransferModel>> LoginRequest([FromBody] SMSRequestViewModel input)
        {
            ResultModel<TenantDataTransferModel> resultModel = new ResultModel<TenantDataTransferModel>();

            var loginHistory = new LoginHistoryBusinessModel();
            try
            {
                var userAction = new UserInfoAction();
                var sendSMSService = new SendSMSService();
                
                var mobileNumber = input.MobileNumber;
                Random rnd = new Random();
                var disposablePassword = rnd.Next(13024, 100000);
                var message = "سیستم مدیریت جهان ساتراپ" + Environment.NewLine +
                              "رمز یکبار مصرف شما: " + disposablePassword.ToString() + Environment.NewLine +
                                "گروه نرم افزاری نوت";
                //var attachmentAction = new AttachmentAction(userAction.FactoryContainer);
                var users = await userAction.GetAll(x => x.MobileNo == mobileNumber, "", "UserTenants");
                if (users.Count == 0)
                    throw new ArgumentException(Errors.UserNotFound, "original");

                var user = await userAction.Get(users.FirstOrDefault().UserInfoId.Value);
                user.ActivationCode = disposablePassword.ToString();
                userAction.Modify(user);
                List<String> str = new List<string>();
                var sendData = sendSMSService.Send(input.MobileNumber, message);
                if (sendData)
                {
                    TenantAction tenantAction = new TenantAction();
                    var tenants = user.UserTenants.Select(x => x.TenantId.Value).ToList();
                    var result = await tenantAction.GetAll(x => tenants.Contains(x.TenantId.Value));
                    resultModel.Success = true;
                    resultModel.TotalCount = result.Count;
                    resultModel.Items = result;
                }
                else
                {
                    resultModel.Success = false;
                    resultModel.Error = "کاربر دسترسی به هیچ شرکتی ندارد";
                }
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Error = ex.Message;
            }
            return resultModel;
        }

        [HttpGet, HttpOptions]
        public async Task<LoginHistoryDataTransferModel> Authentication([FromRoute] string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var loginHistoryAction = new LoginHistoryAction();
                IList<LoginHistoryDataTransferModel> result = new List<LoginHistoryDataTransferModel>();
                result = await loginHistoryAction.GetAll();
                result = result.Where(x => x.Token == guid).ToList();
                if (result.Count == 0)
                    return null;

                if (result.FirstOrDefault()?.ExpireDateTime <= DateTime.Now)
                    return null;

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }

        [HttpGet]
        public async Task<LoginHistoryDataTransferModel> Logout([FromBody] Guid input)
        {

            var loginHistory = new LoginHistoryDataTransferModel();
            try
            {
                var loginHistoryAction = new LoginHistoryAction();
                IList<LoginHistoryDataTransferModel> result = new List<LoginHistoryDataTransferModel>();
                result = await loginHistoryAction.GetAll();
                result = result.Where(x => x.Token == input && x.LogoutDateTime == null).ToList();
                if (result.Count == 0)
                    return null;
                var last = await loginHistoryAction.Get(result.FirstOrDefault().LoginHistoryId.Value);
                last.LogoutDateTime = DateTime.Now;
                loginHistoryAction.Modify(last);
                return null;
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }

        }

        private LoginHistoryBusinessModel PostLoginHistory(LoginHistoryBusinessModel input)
        {
            try
            {
                var action = new LoginHistoryAction();
                action.Add(input);
                return input;
            }
            catch (Exception ex)
            {
                throw ServerException(ex);
            }
        }
        
    }
}