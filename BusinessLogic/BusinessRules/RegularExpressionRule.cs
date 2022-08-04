using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public class RegularExpressionRule<T> : BusinessRuleBase<T> where T : class
    {

        public string Pattern { get; private set; }

        public RegularExpressionRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, string pattern)
            : base(errorMessage, states, attributes)
        {
            Pattern = pattern;
        }

        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                if (!HasState(state))
                    return true;

                foreach (var attribute in Attributes.Where(x => x(inputObject) != null))
                    if (!Regex.IsMatch(attribute(inputObject).ToString(), Pattern))
                        return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
