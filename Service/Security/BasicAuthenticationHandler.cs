using BusinessModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessLogic.Action;

namespace Service.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Path.ToString().ToLower() != "/api/Security/LoginRequest".ToLower() && Request.Path.ToString().ToLower() != "/api/Security/UserLogin".ToLower() && Request.Path.ToString().ToLower() != "/api/Security/MobileLogin".ToLower() && Request.Path.ToString().ToLower() != "/api/ProductManagement/ProductBenefit".ToLower() && !Request.Path.ToString().ToLower().StartsWith("/api/ProductManagement/BuyProfit".ToLower()))
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail(Resources.Errors.MissingAuthorizationHeader);

                try
                {
                    var userAction = new UserInfoAction();
                    var loginHistoryAction = new LoginHistoryAction();
                    var authHeader = Request.Headers["Authorization"];
                    var credentials = authHeader[0].Split(new[] { ' ' }, 2);
                    var token = Guid.Parse(credentials[1]);

                    var loginHistory = await loginHistoryAction.GetAll(x => x.Token == token);

                    loginHistory = loginHistory.ToList();
                    if (loginHistory == null || loginHistory.Count == 0)
                        throw new Exception();


                    StaticData.UserInfo.CurrentUser = new UserInfoBusinessModel();
                    StaticData.UserInfo.CurrentUser.UserInfoId = loginHistory.FirstOrDefault(model => model.Token == token)?.UserId;
                    if (!loginHistory.Any(model => model.Token.ToString() == token.ToString()))
                        throw new Exception();
                }
                catch (Exception)
                {
                    return AuthenticateResult.Fail(Resources.Errors.InvalidAuthorizationHeader);
                }
            }
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, ""),
                new Claim(ClaimTypes.Name, ""),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
