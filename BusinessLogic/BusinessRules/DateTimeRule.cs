using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public class DateTimeRule<T> : BusinessRuleBase<T> where T : class
    {

        private object MinVal { get; set; }

        private object MaxVal { get; set; }

        public DateTimeRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, string minVal, string maxVal)
            : base(errorMessage, states, attributes)
        {
            MinVal = minVal;
            MaxVal = maxVal;
        }

        public DateTimeRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, DateTime minVal, DateTime maxVal)
            : base(errorMessage, states, attributes)
        {
            MinVal = minVal;
            MaxVal = maxVal;
        }


        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                if (!HasState(state))
                    return true;

                foreach (var attribute in Attributes.Where(x => x(inputObject) != null))
                {

                    if (MinVal.GetType() == typeof(System.DateTime) && MaxVal.GetType() == typeof(System.DateTime))
                        if ((DateTime)attribute(inputObject) < (DateTime)MinVal || (DateTime)attribute(inputObject) > (DateTime)MaxVal)
                            return false;

                    else if (MinVal.GetType() == typeof(System.String) && MaxVal.GetType() == typeof(System.String))
                    {
                        if (attribute(inputObject).ToString().Length != 10)
                            return false;

                        var items = attribute(inputObject).ToString().Split('/');
                        if (items.Length != 3)
                            return false;

                        if (items[0].Length != 4 || items[1].Length != 2 || items[2].Length != 2)
                            return false;

                        int year, month, day;
                        if (!(int.TryParse(items[0], out year)) || !(int.TryParse(items[1], out month)) || !(int.TryParse(items[2], out day)))
                            return false;

                        if (year < 1300 || month < 1 || month > 12 || day < 1)
                            return false;

                        if (month > 6 && day > 30)
                            return false;

                        if (month <= 6 && day > 31)
                            return false;

                        if (!IsLeapYear(year) && month == 12 && day > 29)
                            return false;

                        if (attribute(inputObject).ToString().CompareTo(MinVal) < 0 || attribute(inputObject).ToString().CompareTo(MaxVal) > 0)
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsLeapYear(int year)
        {
            int mode = year % 33;
            return (mode == 1 || mode == 5 || mode == 9 || mode == 13 || mode == 17 || mode == 22 || mode == 26 || mode == 30);

        }

    }
}
