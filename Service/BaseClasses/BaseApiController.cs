using BusinessLogic.Action;
using Common.DTO;
using Common.Enums;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using BusinessModel;
using Microsoft.AspNetCore.Http;
using Service.Security;
using Microsoft.AspNetCore.Authorization;

namespace Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        [HttpGet]
        public async Task<long?> GetCurrentUserId()
        {
            try
            {
                // در اینجا توکنی که ارسال شده را بررسی میکند
                //اگر صحت داشت آیدی یوزر که با اون توکن اومده رو ارسال میکنه
                // اگر اون توکن درست نبود برگشت میخوره
                var userAction = new UserInfoAction();
                var loginHistoryAction = new LoginHistoryAction();
                var authHeader = Request.Headers["Authorization"];
                var credentials = authHeader[0].Split(new[] { ' ' }, 2);
                var token = Guid.Parse( credentials[1]);

                var loginHistory = await loginHistoryAction.GetAll(x=> x.Token == token);

                loginHistory = loginHistory.ToList();
                if (loginHistory == null)
                    throw new Exception();

                
                return loginHistory.FirstOrDefault(model => model.Token == token)?.UserId;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected bool HasPaging(DataFilterWithPaging model)
        {
            if (model == null)
                model = new DataFilterWithPaging();

            return !(model.PageNumber == 0 &&
                        model.PageSize == 0);

        }

        protected ArgumentException ServerException(Exception ex)
        {

            //ExceptionHandler.Log(ex);
            return new ArgumentException(ex.Message);
        }

        public Expression<Func<T, bool>> GetFilterExpression<T>(Filter[] filters)
        {
            if (filters == null || filters.Count() == 0)
                return null;

            List<Expression> filterExpressions = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(T), "x");
            foreach (var item in filters)
                filterExpressions.Add(GetExpression(typeof(T), item.Property, item.Operation, item.Values, parameterExpr));

            Expression result = filterExpressions[0];

            for (int index = 1; index < filterExpressions.Count; index++)
                result = Expression.And(result, filterExpressions[index]);

            return Expression.Lambda<Func<T, bool>>(result, parameterExpr);
        }

        public Expression<Func<T, bool>> GetSuggestionExpression<T>(Filter[] filters)
        {
            if (filters == null || filters.Count() == 0)
                return null;

            List<Expression> filterExpressions = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(T), "x");
            foreach (var item in filters)
                filterExpressions.Add(GetExpression(typeof(T), item.Property, item.Operation, item.Values, parameterExpr));

            Expression result = filterExpressions[0];

            for (int index = 1; index < filterExpressions.Count; index++)
                result = Expression.Or(result, filterExpressions[index]);

            return Expression.Lambda<Func<T, bool>>(result, parameterExpr);
        }

        public Expression<Func<T, bool>> AddFilterExpression<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            Expression<Func<T, bool>> result = null;
            if (left != null)
                if (right == null)
                    result = left;
                else
                    result = Expression.Lambda<Func<T, bool>>(Expression.And(left.Body, right.Body), left.Parameters);
            else
                result = right;
            return result;
        }

        private static Expression GetExpression(Type objectType, string propertyPath, FilterOperation operation, string[] values, Expression parameter)
        {
            var memberAccess = GetMethodAccessExpr(objectType, propertyPath, operation, values, parameter);
            if (memberAccess.NodeType == ExpressionType.Call)
                return memberAccess;

            Expression convertExpression;
            List<Expression> operationExprs = new List<Expression>();
            Expression result;

            switch (operation)
            {
                case FilterOperation.Contains:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var methodInfo = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                        var expr = Expression.Call(memberAccess, methodInfo, Expression.Constant(values[index]));
                        operationExprs.Add(expr);
                    }

                    break;
                case FilterOperation.EndsWith:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var methodInfo = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                        var expr = Expression.Call(memberAccess, methodInfo, Expression.Constant(values[index]));
                        operationExprs.Add(expr);
                    }

                    break;
                case FilterOperation.Equal:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        Expression expr = Expression.Equal(memberAccess, convertExpression);
                        if (string.IsNullOrEmpty(values[index]))
                            expr = Expression.Or(expr, Expression.Equal(memberAccess, Expression.Constant(null)));
                        operationExprs.Add(expr);
                    }

                    break;
                case FilterOperation.Greater:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        operationExprs.Add(Expression.GreaterThan(memberAccess, convertExpression));
                    }

                    break;
                case FilterOperation.GreaterOrEqual:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        operationExprs.Add(Expression.GreaterThanOrEqual(memberAccess, convertExpression));
                    }

                    break;
                case FilterOperation.Less:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        operationExprs.Add(Expression.LessThan(memberAccess, convertExpression));
                    }

                    break;
                case FilterOperation.LessOrEqual:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        operationExprs.Add(Expression.LessThanOrEqual(memberAccess, convertExpression));
                    }

                    break;
                case FilterOperation.NotContains:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var methodInfo = typeof(string).GetMethod("NotContains", new Type[] { typeof(string) });
                        var expr = Expression.Call(memberAccess, methodInfo, Expression.Constant(values[index]));
                        operationExprs.Add(Expression.NotEqual(expr, Expression.Constant(true)));
                    }

                    break;
                case FilterOperation.NotEqual:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        Expression expr = Expression.NotEqual(memberAccess, convertExpression);
                        if (string.IsNullOrEmpty(values[index]))
                            expr = Expression.And(expr, Expression.NotEqual(memberAccess, Expression.Constant(null)));
                        operationExprs.Add(expr);

                    }

                    break;
                case FilterOperation.StartsWith:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var methodInfo = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                        var expr = Expression.Call(memberAccess, methodInfo, Expression.Constant(values[index]));
                        operationExprs.Add(expr);
                    }

                    break;
                default:
                    for (int index = 0; index < values.Length; index++)
                    {
                        var convertedValue = TypeConvertor(values[index], memberAccess.Type);
                        convertExpression = Expression.Convert(Expression.Constant(convertedValue), memberAccess.Type);
                        Expression expr = Expression.Equal(memberAccess, convertExpression);
                        if (string.IsNullOrEmpty(values[index]))
                            expr = Expression.Or(expr, Expression.Equal(memberAccess, Expression.Constant(null)));
                        operationExprs.Add(expr);
                    }

                    break;
            }

            result = operationExprs[0];

            for (int index = 1; index < operationExprs.Count; index++)
                result = Expression.Or(result, operationExprs[index]);

            return result;
        }

        private static Expression GetMethodAccessExpr(Type objectType, string propertyPath, FilterOperation operation, string[] values, Expression parameter)
        {
            PropertyInfo propertyInfo = null;
            string[] inputSplit = propertyPath.Split('.');
            for (var index = 0; index < inputSplit.Count(); index++)
            {
                propertyInfo = objectType.GetProperty(inputSplit[index]);
                if (propertyInfo != null)
                {
                    objectType = propertyInfo.PropertyType;
                    if (objectType.Namespace.ToLower().Contains("system.collections"))
                    {
                        Type propertyUnderlyingType = objectType.GenericTypeArguments[0];
                        var anyExpressionParameter = Expression.Parameter(propertyUnderlyingType, "y");
                        propertyPath = string.Empty;
                        for (int j = index + 1; j < inputSplit.Count(); j++)
                        {
                            propertyPath += inputSplit[j];
                            if (j + 1 < inputSplit.Count())
                                propertyPath += ".";
                        }
                        var anyExpression = GetExpression(propertyUnderlyingType, propertyPath, operation, values, anyExpressionParameter);
                        parameter = Expression.Property(parameter, propertyInfo);
                        var anyMethodInfo = typeof(Enumerable).GetMethods().Where(x => x.Name == "Any").ToList()[1].MakeGenericMethod(propertyUnderlyingType);
                        return Expression.Call(null, anyMethodInfo, new Expression[] { parameter, Expression.Lambda(anyExpression, anyExpressionParameter) });
                    }
                    else
                        parameter = Expression.Property(parameter, propertyInfo);
                }
            }

            return parameter;
        }

        private static object TypeConvertor(string value, Type type)
        {
            Type convertedType = type;
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                convertedType = Nullable.GetUnderlyingType(convertedType);
                var result = Convert.ChangeType(value, convertedType);
                return result;
            }
            else
                return Convert.ChangeType(value, type);
        }
    }
}
