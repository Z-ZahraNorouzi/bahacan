//using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.MS_SQL
{
    public static class Extensions
    {
        //private static KeyValuePair<Type, object>[] ResolveArgs(Expression<Func<T, object>> expression)
        //{
        //    var body = (System.Linq.Expressions.MethodCallExpression)expression.Body;
        //    var values = new List<KeyValuePair<Type, object>>();

        //    foreach (var argument in body.Arguments)
        //    {
        //        var exp = ResolveMemberExpression(argument);
        //        var type = argument.Type;

        //        var value = GetValue(exp);

        //        values.Add(new KeyValuePair<Type, object>(type, value));
        //    }

        //    return values.ToArray();
        //}

        public static MemberExpression ResolveMemberExpression(Expression? expression)
        {

            if (expression is MemberExpression)
            {
                return (MemberExpression)expression;
            }
            else if (expression is UnaryExpression)
            {
                // if casting is involved, Expression is not x => x.FieldName but x => Convert(x.Fieldname)
                return (MemberExpression)((UnaryExpression)expression).Operand;
            }
            else
            {
                throw new NotSupportedException(expression?.ToString());
            }
        }

        private static object? GetValue(MemberExpression exp)
        {
            // expression is ConstantExpression or FieldExpression
            if (exp.Expression!= null && exp.Expression is ConstantExpression)
            {
                return (((ConstantExpression)exp.Expression).Value)?
                        .GetType()?
                        .GetField(exp.Member.Name)?
                        .GetValue(((ConstantExpression)exp.Expression).Value);
            }
            else if (exp.Expression is MemberExpression)
            {
                return GetValue((MemberExpression)exp.Expression);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static Expression<Func<TDestination, bool>>? ConvertExpression<TSource, TDestination>(Expression<Func<TSource, bool>>? filter)
        {
            if (filter == null)
                return null;

            List<ParameterExpression> parameters = new List<ParameterExpression>();
            foreach (var parameter in filter.Parameters)
                parameters.Add(Expression.Parameter(typeof(TDestination), parameter.Name));


            var expr = ConvertExpression(filter.Body, null, parameters, typeof(TSource));
            if (expr != null)
                return Expression.Lambda<Func<TDestination, bool>>(expr, parameters.ToArray());
            else
                return null;

        }

        private static Expression? ConvertExpression(Expression? input, object? inputVariable, List<ParameterExpression>? parameters, Type sourceType)
        {
            if (input == null)
                return null;

            if (input is BinaryExpression)
            {
                BinaryExpression? inputExpr = input as BinaryExpression;
                Expression? left = ConvertExpression(inputExpr?.Left, null, parameters, sourceType);
                Expression? right = ConvertExpression(inputExpr?.Right, left?.Type, parameters, sourceType);
                return Expression.MakeBinary(input.NodeType, left, right);
            }
            else if (input.NodeType == ExpressionType.Constant)
            {
                ConstantExpression innerExpr = input as ConstantExpression;
                //object value = innerExpr.Value;
                //var k = ( as FieldInfo).GetValue(((input as MemberExpression).Expression as ConstantExpression).Value);
                /// Type type = innerExpr.Value.GetType();
                //if (type.IsDefined (typeof (CompilerGeneratedAttribute), false))
                //{
                //    //string value = CharacterCorrector.ReplacePersianCodePages((input as ConstantExpression).Value.ToString());
                //    return Expression.Constant(value, typeof(string));
                //}
                //else
                return input;
            }
            else if (input.NodeType == ExpressionType.Convert)
            {
                UnaryExpression inputExpr = input as UnaryExpression;
                return Expression.Convert(inputExpr.Operand, (Type)inputVariable);
            }
            else if (input.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression inputExpression = input as MemberExpression;
                Expression innerExpression = ConvertExpression(inputExpression.Expression, null, parameters, sourceType);
                if (innerExpression.NodeType == ExpressionType.Constant)
                    return input;

                string propertyName = inputExpression.Member.Name;
                string mappedName1 = DaoMapper.Map(sourceType, propertyName);
                string mappedName = DaoMapper.Map(inputExpression.Expression.Type, propertyName);
                Expression property = innerExpression;

                foreach (var item in mappedName.Split('.'))
                {
                    if (property.Type.Namespace.ToLower().Contains("System.Collections".ToLower()))
                    {
                        var parameterType = property.Type.GenericTypeArguments[0];
                        var successorPropertyType = parameterType.GetProperty(item).PropertyType;
                        var successorPropertyGenericType = successorPropertyType.GenericTypeArguments[0];
                        var parameterExpr = Expression.Parameter(parameterType);
                        var parameterProperty = Expression.Property(parameterExpr, item);
                        Expression selectfunc = Expression.Lambda(parameterProperty, new[] { parameterExpr });

                        var method = typeof(Enumerable).GetMethods().Where(x => x.Name == "SelectMany").ToList()[0];
                        method = method.MakeGenericMethod(parameterType, successorPropertyGenericType);
                        property = Expression.Call(method, new[] { property, selectfunc });
                    }
                    else
                    {
                        if (property.Type.GetProperty(item) != null)
                            property = Expression.Property(property, item);
                    }
                }
                return property;
            }
            else if (input.NodeType == ExpressionType.Parameter)
            {
                ParameterExpression inputExpression = input as ParameterExpression;
                var parameter = parameters.Where(x => ((ParameterExpression)x).Name == inputExpression.Name).FirstOrDefault();
                if (parameter == null)
                {
                    var parameterType = DaoMapper.Map(input.Type);
                    return Expression.Parameter(parameterType, inputExpression.Name);
                }
                else
                    return parameter;
            }
            else if (input.NodeType == ExpressionType.Call)
            {
                MethodCallExpression inputExpression = input as MethodCallExpression;
                List<Expression> args = new List<Expression>();
                List<Type> genericArgs = new List<Type>();
                foreach (var arg in inputExpression.Arguments)
                    args.Add(ConvertExpression(arg, null, parameters, sourceType));
                foreach (var arg in inputExpression.Method.GetGenericArguments())
                    genericArgs.Add(DaoMapper.Map(arg));


                Expression instance = ConvertExpression(inputExpression.Object, null, parameters, sourceType);
                if (instance == null)
                {
                    var methods = inputExpression.Method.DeclaringType.GetMethods();
                    methods = methods.Where(x => x.Name == inputExpression.Method.Name).ToArray();
                    MethodInfo methodInfo;
                    foreach (var method in methods)
                    {
                        if (genericArgs.Count() > 0)
                            methodInfo = method.MakeGenericMethod(genericArgs.ToArray());
                        else
                            methodInfo = method;
                        var params1 = methodInfo.GetParameters().Select(x => x.ParameterType).ToList();
                        var params2 = args.Select(x => x.Type).ToList();

                        if (params1.Count != params2.Count)
                            continue;

                        bool compareFlag = true;
                        for (int index = 0; index < params1.Count; index++)
                        {
                            Type param1Type;
                            Type param2Type;
                            bool param1TypeisArray = false;
                            bool param2TypeisArray = false;

                            if (params1[index].GenericTypeArguments.Count() > 0)
                                param1Type = params1[index].GenericTypeArguments[0];
                            else
                                param1Type = params1[index];

                            if (params2[index].GenericTypeArguments.Count() > 0)
                                param2Type = params2[index].GenericTypeArguments[0];
                            else
                                param2Type = params2[index];

                            if (params1[index].IsArray)
                                param1Type = params1[index].GetElementType();

                            if (params2[index].IsArray)
                                param2Type = params2[index].GetElementType();

                            if (params1[index].Namespace.ToLower().Contains("System.Collections".ToLower()) || params1[index].IsArray)
                                param1TypeisArray = true;

                            if (params2[index].Namespace.ToLower().Contains("System.Collections".ToLower()) || params2[index].IsArray)
                                param2TypeisArray = true;

                            if (!(param1TypeisArray == param2TypeisArray && param1Type == param2Type))
                                compareFlag = false;
                        }

                        if (compareFlag)
                            return Expression.Call(methodInfo, args.ToArray());
                    }
                }
                else
                    if (inputExpression.Method.GetGenericArguments().Count() > 0)
                        return Expression.Call(instance, inputExpression.Method.Name, inputExpression.Method.GetGenericArguments(), args.ToArray());
                    else
                        return Expression.Call(instance, inputExpression.Method, args.ToArray());
            }
            else if (input.NodeType == ExpressionType.Lambda)
            {
                LambdaExpression inputExpression = input as LambdaExpression;
                List<ParameterExpression> pars = new List<ParameterExpression>();
                List<ParameterExpression> allPars = new List<ParameterExpression>();
                foreach (var par in inputExpression.Parameters)
                {
                    var convertedParameter = ConvertExpression(par, null, parameters, sourceType);
                    if (convertedParameter != null)
                    {
                        pars.Add((ParameterExpression)convertedParameter);
                        allPars.Add((ParameterExpression)convertedParameter);
                    }
                }

                foreach (var par in parameters)
                    if (allPars.Where(x => x.Equals(par)).Count() == 0)
                        allPars.Add(par);

                var expr = ConvertExpression(inputExpression.Body, null, allPars, sourceType);
                return Expression.Lambda(expr, pars.ToList());
            }

            return null;
        }

        public static IQueryable<T> OrderBy<T>(string orderByExpression, IQueryable<T> items)
        {
            if (string.IsNullOrEmpty(orderByExpression))
                return items;

            string[] expressions = orderByExpression.Split(',');
            var parameter = Expression.Parameter(typeof(T), "p");
            int index = 0;

            foreach (string expr in expressions)
            {
                index++;
                string[] exprSplit = expr.Split(' ').Where(x => x != string.Empty).ToArray();
                if (exprSplit.Length == 0) continue;
                string propertyName = exprSplit[0];
                string orderbyMethod = "OrderBy";
                var propertyAccessExpr = GetPropertyAccessExpr<T>(propertyName, parameter);
                var orderExpression = Expression.Lambda(propertyAccessExpr, parameter);
                orderbyMethod = (exprSplit.Length == 2 && exprSplit[1].ToLower() == "desc") ? "OrderByDescending" : "OrderBy";
                orderbyMethod = index == 1 ? orderbyMethod : orderbyMethod.Replace("OrderBy", "ThenBy");
                var expression = Expression.Call(typeof(Queryable), orderbyMethod, new[] { items.ElementType, orderExpression.ReturnType }, items.Expression, orderExpression);
                items = items.Provider.CreateQuery<T>(expression);
            }

            return items;
        }

        private static Expression GetPropertyAccessExpr<T>(string propertyName, Expression parameter)
        {
            var typeOfT = typeof(T);
            var sourceType = DaoMapper.Map(typeOfT);
            PropertyInfo pInfo = null;

            foreach (string str in propertyName.Split('.'))
            {
                var mappedParameterPath = DaoMapper.Map(sourceType, str);
                foreach (string str2 in mappedParameterPath.Split('.'))
                {
                    pInfo = typeOfT.GetProperty(str2);
                    typeOfT = pInfo.PropertyType;
                    if (typeOfT.GenericTypeArguments.Count() > 0)
                        typeOfT = typeOfT.GenericTypeArguments.FirstOrDefault();
                    if (pInfo != null)
                        parameter = Expression.Property(parameter, pInfo);
                }
                sourceType = DaoMapper.Map(typeOfT);
            }

            return parameter;
        }

    }

}