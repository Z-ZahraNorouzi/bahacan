using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class BusinessModelBase
    {
        public string[] GetProperties()
        {
            return GetProperties(this.GetType());
        }

        private string[] GetProperties(Type input, string prefix = "")
        {
            List<string> result = new List<string>();
            List<PropertyInfo> properties = new List<PropertyInfo>();
            properties = input.GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance).ToList();

            foreach (var item in properties)
            {
                var itemType = item.PropertyType;
                var propertyName = string.IsNullOrEmpty(prefix) ? item.Name : string.Concat(prefix, ".", item.Name);
                string[] propertyNameSplit = propertyName.Split('.');
                string propertyNameLastPart = string.Empty;
                if (propertyNameSplit.Length > 1)
                    propertyNameLastPart = string.Concat(propertyNameSplit[propertyNameSplit.Length - 2], ".", propertyNameSplit[propertyNameSplit.Length - 1]);

                if (itemType.IsGenericType)
                    itemType = itemType.GenericTypeArguments[0];
                if (!itemType.IsValueType && itemType != typeof(string) && (!prefix.Contains(propertyNameLastPart) || string.IsNullOrEmpty(prefix)))
                    result.AddRange(GetProperties(itemType, propertyName));
                else if (itemType.IsValueType || itemType == typeof(string))
                    result.Add(propertyName);
            }
            return result.ToArray();
        }

        public object? GetPropertyValue(string propertyName)
        {
            var isContainer = false;
            var properties = propertyName.Split('.');
            List<object?> value = new List<object?>();
            List<object?> tempValue = new List<object?>();
            var typeOf = this.GetType();
            value.Add(this);

            foreach (var item in properties)
            {
                var propInfo = typeOf.GetProperty(item);
                if (propInfo == null)
                    return null;

                if (propInfo.PropertyType.GenericTypeArguments.Count() > 0)
                    typeOf = propInfo.PropertyType.GenericTypeArguments[0];
                else
                    typeOf = propInfo.PropertyType;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (propInfo.PropertyType.Namespace.ToLower().Contains("System.Collections".ToLower()))
                    isContainer = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                tempValue.Clear();
                foreach (var val in value)
                {
                    if (val != null)
                        tempValue.Add(propInfo.GetValue(val));
                    else
                        tempValue.Add(val);
                }
                value.Clear();
                foreach (var val in tempValue)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (val != null && val.GetType().Namespace.ToLower().Contains("System.Collections".ToLower()))
                        value.AddRange((IEnumerable<object>)val);
                    else
                    {
                        if (!value.Contains(val))
                            value.Add(val);
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            }

            if (isContainer)
                return value;
            else
                return value.FirstOrDefault();
        }
    }
}
