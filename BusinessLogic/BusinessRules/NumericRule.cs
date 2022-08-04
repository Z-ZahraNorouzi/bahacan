using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public class NumericRule<T> : BusinessRuleBase<T> where T : class
    {

        private double? MinVal { get; set; }

        private double? MaxVal { get; set; }

        private int? Scale { get; set; }

        public NumericRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, int? scale, double? minVal = null, double? maxVal = null)
            : base(errorMessage, states, attributes)
        {
            MinVal = minVal;
            MaxVal = maxVal;
            Scale = scale;
        }

        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                string pattern = @"^[-,+]?[\d]*";
                string value = string.Empty;

                if (Scale.HasValue && Scale.Value > 0)
                {
                    pattern += ".";
                    pattern += string.Format(@"\d{0}", "{0," + Scale.Value.ToString() + "}");
                }
                pattern += "$";


                if (!HasState(state))
                    return true;

                foreach (var attribute in Attributes.Where(x => x(inputObject) != null))
                {
                    value = attribute(inputObject).ToString();
                    if (!Regex.IsMatch(value, pattern) || value == "-." || value == "+." || value == ".")
                        return false;

                    double NumericValue = double.Parse(value);
                    if (MinVal != null && NumericValue < MinVal)
                        return false;
                    if (MaxVal != null && NumericValue > MinVal)
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
