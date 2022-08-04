using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class CharacterCorrector
    {

        public static void ReplacePersianCodePages(BusinessModelBase input)
        {
            if (input != null)
            {
                Type typeOfInput = input.GetType();
                foreach (PropertyInfo property in typeOfInput.GetProperties().Where(x => x.CanWrite == true))
                {
                    if (property.PropertyType == typeof(string) && property.GetValue(input) != null)
                    {
                        string? value = property.GetValue(input)?.ToString();
                        property.SetValue(input, ReplacePersianCodePages(value));
                    }
                }
            }
        }

        public static string ReplacePersianCodePages(string? input)
        {
            if (string.IsNullOrEmpty(input))
                input = String.Empty;
            return input.Replace("ي", "ی").Replace("ك", "ک").Replace("و", "و").Replace("ة", "ه");
        }

    }
}
