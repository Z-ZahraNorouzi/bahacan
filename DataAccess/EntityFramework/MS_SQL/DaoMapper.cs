using BusinessModel;
using DataAccess.EntityFramework.Model;
using DataTransferModel.Security;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.MS_SQL
{
    public static class DaoMapper
    {
        private static ConcurrentBag<string> mapCollection = new ConcurrentBag<string>();

        public static string Map(Type sourceType, string propertyNames)
        {
            string result = string.Empty;
            string temp1;


            if (string.IsNullOrEmpty(propertyNames))
                return string.Empty;

            if (propertyNames.ToLower() == "ignoreInclude".ToLower())
                return string.Empty;


            foreach (var item in propertyNames.Split(','))
            {
                string itemFullName = sourceType.Name + "." + item;
                string[] itemSplit = item.Split('.');
                temp1 = string.Empty;

                if (!string.IsNullOrEmpty(result))
                    result += ",";

                if (itemSplit.Length > 1)
                {
                    var propertyInfo = sourceType.GetProperty(itemSplit[0]);
                    Type propertyType;
                    if (propertyInfo == null)
                        continue;

                    if (propertyInfo.PropertyType.GenericTypeArguments != null && propertyInfo.PropertyType.GenericTypeArguments.Count() > 0)
                        propertyType = propertyInfo.PropertyType.GenericTypeArguments[0];
                    else
                        propertyType = propertyInfo.PropertyType;

                    for (var index = 1; index < itemSplit.Length; index++)
                        temp1 += "." + itemSplit[index];
                    if (temp1.StartsWith("."))
                        temp1 = temp1.Substring(1, temp1.Length - 1);

                    result += Map(sourceType, itemSplit[0]) + ".";
                    result += Map(propertyType, temp1);
                }
                else
                {
                    var map = mapCollection.Where(x => x.Contains(itemFullName + ";") || x.Contains(";" + itemFullName)).FirstOrDefault();

                    if (map != null)
                    {
                        string[] mapSplit = map.Split(';');

                        if (mapSplit.Length > 1)
                            if (mapSplit[0] == itemFullName)
                                temp1 = mapSplit[1];
                            else
                                temp1 = mapSplit[0];
                        else
                            temp1 = mapSplit[0];

                        for (int i = 1; i < temp1.Split('.').Length; i++)
                        {
                            if (i > 1)
                                result += ".";

                            result += temp1.Split('.')[i];
                        }
                    }
                    else
                        result += item;
                }
            }

            return result.Replace(".,", ",");
        }
        
        public static Type? Map(Type input)
        {
            Type result = input;
            switch (input.Name)
            {

                case "UserInfoBusinessModel":
                    result = typeof(UserInfo);
                    break;
                case "UserInfo":
                    result = typeof(UserInfoBusinessModel);
                    break;
                case "UserInfoDataTransferModel":
                    result = typeof(UserInfoBusinessModel);
                    break;
                default:
                    break;
            }
            return result;
        }
        
    }
}
