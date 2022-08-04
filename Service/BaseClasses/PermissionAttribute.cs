
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Service.BaseClasses
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private string[] _selectedPermissions;

        public PermissionAttribute(params string[] permissionsTitles)
        {
            _selectedPermissions = permissionsTitles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null) return;

            var hasPermission = false;

            var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).ToList();


            var request = context.HttpContext.Request;
            string controller = request.RouteValues["controller"].ToString();

                if (userRoles.Any(s => s.Value == controller))
                    hasPermission = true;

            if (!hasPermission)
                  throw new Exception();
        }

        
    }
}
