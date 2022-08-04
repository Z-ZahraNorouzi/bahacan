using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Action;
using BusinessModel;
using Microsoft.AspNetCore.Http;

namespace Service.Security
{
    public static class AuthorizeSecurity
    {

        internal static void Authentication()
        {

            //if (HttpContext.Current.Request.RequestType == "OPTIONS")
            //    return;

            //Guid? token = HttpContext.Current.Request.Headers["X-Token"] != null ? Guid.Parse(HttpContext.Current.Request.Headers["X-Token"]) : new Guid();
            //string userName = HttpContext.Current.Request.Headers["X-User"] != null ? (HttpContext.Current.Request.Headers["X-User"]).ToString() : string.Empty;

            //if (string.IsNullOrEmpty(userName))
            //    throw new HttpResponseException(HttpStatusCode.Forbidden);

            //var userAction = new UserAction();
            //var user = userAction.GetByUserName(userName);
            //StaticData.UserInfo.CurrentUser = user;
            //var loginHistoryAction = new LoginHistoryAction();

            //var loginHistory = loginHistoryawait action.GetAll();

            //var lastLoginToken = loginHistory.Where(x => x.LogoutDateTime == null && x.UserId == user.UserId.Value && x.Token == token).ToList();
            //if (lastLoginToken.Count() == 0)
            //    throw new HttpResponseException(HttpStatusCode.Forbidden);

            //if (lastLoginToken.FirstOrDefault().ExpireDateTime < DateTime.Now)
            //    throw new HttpResponseException(HttpStatusCode.Forbidden);

            return;

        }

        internal static void Authorize(Permissions permissions)
        {
            try
            {
                //var token = HttpContext.Current.Request.Headers["X-Token"] != null ? Guid.Parse(HttpContext.Current.Request.Headers["X-Token"]) : new Guid();
                //var result = false;
                //var loginHistoryAction = new LoginHistoryAction();
                //var permissionAction = new UserPermissionAction();
                //var users = loginHistoryawait action.GetAll(x => x.LogoutDateTime == null && x.Token == token).Select(x=> x.UserId).ToList();
                var permissionList = new List<UserPermissionBusinessModel>();
                //if (users.Count > 0)
                //{
                //    var permissionValue = (int)permissions;
                //    permissionList = permissionawait action.GetAll(x => x.UserId == users.FirstOrDefault().Value && x.PermissionId == permissionValue).ToList();
                //    if (permissionList.Count > 0)
                //        result = true;
                //}
                //if (!result)
                //    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}