using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common.DTO
{
    public class Extension
    {
        public static Int16? AsInt16(Object item, Int16? defaultValue = null)
        {
            Int16 result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!Int16.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static Byte? AsByte(Object item, Byte? defaultValue = null)
        {
            Byte result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!Byte.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }
        public static Byte[] AsByteArray(Object item, Byte[] defaultValue = null)
        {
            Byte[] result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            result = Encoding.ASCII.GetBytes(item.ToString());

            return result;
        }

        public static Int32? AsInt32(object item, Int32? defaultValue = null)
        {
            Int32 result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!Int32.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static Int64? AsInt64(object item, Int64? defaultValue = null)
        {
            Int64 result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!Int64.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static short? AsShort(object item, short? defaultValue = null)
        {
            short result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!short.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static long? AsLong(object item, long? defaultValue = null)
        {
            long result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!long.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static decimal? AsDecimal(object item, decimal? defaultValue = null)
        {
            decimal result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!decimal.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static double? AsDouble(object item, double? defaultValue = null)
        {
            double result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!double.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static float? AsFloat(object item, float? defaultValue = null)
        {
            float result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!float.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static string AsString(object item, string defaultValue = "")
        {

            if (item == null || DBNull.Value == item)
                return defaultValue;

            return item.ToString();
        }

        public static string AsStringDefNull(object item, string defaultValue = null)
        {

            if (item == null || DBNull.Value == item)
                return defaultValue;

            return item.ToString();
        }
        public static DateTime? AsDateTime(object item, DateTime? defaultValue = null)
        {

            DateTime result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;

        }

        public static TimeSpan? AsTime(object item, TimeSpan? defaultValue = null)
        {

            TimeSpan result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!TimeSpan.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;

        }

        public static bool? AsBool(object item, bool? defaultValue = null)
        {

            if (item == null || DBNull.Value == item)
                return defaultValue;

            return (new List<string> { "yes", "y", "true", "1" }).Contains(item.ToString().ToLower());

        }

        public static Guid? AsGuid(object item, Guid? defaultValue = null)
        {
            Guid result;

            if (item == null || DBNull.Value == item)
                return defaultValue;

            if (!Guid.TryParse(item.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static string Exppression2Xml(Expression input)
        {
            List<object[]> parsTable = new List<object[]>();
            Exppression2FlatTable((input), parsTable, 0);

            XmlDocument doc = new XmlDocument();
            XmlElement rootElement = doc.CreateElement("Root");
            foreach (var item in parsTable)
            {
                XmlElement rowElement = doc.CreateElement("row");

                rowElement.SetAttribute("ColumnName", item[0].ToString());
                rowElement.SetAttribute("Operator", item[1].ToString());
                rowElement.SetAttribute("FilterValue", item[2].ToString());

                rootElement.AppendChild(rowElement);
            }
            doc.AppendChild(rootElement);
            return doc.InnerXml;
        }

        private static void Exppression2FlatTable(Expression input, List<object[]> parsTable, int index, bool insert = true)
        {
            if (input == null)
                return;

            if (input is BinaryExpression)
            {
                if (insert)
                {
                    parsTable.Add(new object[3]);
                    index = parsTable.Count - 1;
                }
                BinaryExpression innerExpr = input as BinaryExpression;
                Exppression2FlatTable(innerExpr.Left, parsTable, index);
                switch (input.NodeType)
                {
                    case ExpressionType.GreaterThanOrEqual:
                        parsTable[index][1] = parsTable[index][1] == null ? (int)FilterOperation.GreaterOrEqual : parsTable[index][1];
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    case ExpressionType.GreaterThan:
                        parsTable[index][1] = parsTable[index][1] == null ? (int)FilterOperation.Greater : parsTable[index][1];
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    case ExpressionType.LessThanOrEqual:
                        parsTable[index][1] = parsTable[index][1] == null ? (int)FilterOperation.LessOrEqual : parsTable[index][1];
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    case ExpressionType.LessThan:
                        parsTable[index][1] = parsTable[index][1] == null ? (int)FilterOperation.Less : parsTable[index][1];
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    case ExpressionType.Equal:
                        parsTable[index][1] = (parsTable[index][1] == null ? (int)FilterOperation.Equal : parsTable[index][1]);
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    case ExpressionType.NotEqual:
                        parsTable[index][1] = parsTable[index][1] == null ? (int)FilterOperation.NotEqual : parsTable[index][1];
                        Exppression2FlatTable(innerExpr.Right, parsTable, index);
                        break;
                    default:
                        Exppression2FlatTable(innerExpr.Right, parsTable, index, false);
                        break;
                }
            }
            else if (input.NodeType == ExpressionType.Constant)
            {
                ConstantExpression innerExpr = input as ConstantExpression;
                object value = innerExpr.Value;

                if (value.GetType() == typeof(bool) && (bool)value == false && (FilterOperation)parsTable[index][1] == FilterOperation.Contains)
                    parsTable[index][1] = (int)FilterOperation.NotContains;

                if (parsTable[index][2] == null)
                    parsTable[index][2] = value.ToString();
            }
            else if (input.NodeType == ExpressionType.Convert)
            {
                UnaryExpression inputExpr = input as UnaryExpression;
                Exppression2FlatTable(inputExpr.Operand, parsTable, index);
            }
            else if (input.NodeType == ExpressionType.MemberAccess)
            {
                string columnName = string.Empty;
                var innerExpr = input as MemberExpression;
                var strSplit = innerExpr.ToString().Split('.');

                for (int i = 1; i < strSplit.Length; i++)
                {
                    columnName += strSplit[i];
                    if (i != strSplit.Length - 1)
                        columnName += ".";
                }

                parsTable[index][0] = columnName;
            }
            else if (input.NodeType == ExpressionType.Parameter)
            {
            }
            else if (input.NodeType == ExpressionType.Call)
            {
                var innerExpr = input as MethodCallExpression;
                Exppression2FlatTable(innerExpr.Object, parsTable, index);

                if (innerExpr.Method.Name == "StartsWith")
                    parsTable[index][1] = (int)FilterOperation.StartsWith;
                if (innerExpr.Method.Name == "EndsWith")
                    parsTable[index][1] = (int)FilterOperation.EndsWith;
                if (innerExpr.Method.Name == "Contains")
                    parsTable[index][1] = (int)FilterOperation.Contains;

                Exppression2FlatTable(innerExpr.Arguments[0], parsTable, index);
            }
            else if (input.NodeType == ExpressionType.Lambda)
            {
                var lambdaExpr = input as LambdaExpression;
                var body = lambdaExpr.Body;
                Exppression2FlatTable(body, parsTable, index);
            }
        }
    }
}
