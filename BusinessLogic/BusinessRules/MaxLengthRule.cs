using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public class MaxLengthRule<T> : BusinessRuleBase<T> where T : class
    {

        private int MaxLength { get; set; }
        public MaxLengthRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, int maxLength)
            : base(errorMessage, states, attributes)
        {
            MaxLength = maxLength;
        }

        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                if (!HasState(state))
                    return true;

                foreach (var attribute in Attributes.Where(x => x(inputObject) != null))
                    if (attribute(inputObject).ToString().Length > MaxLength)
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
