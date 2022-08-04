using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public class NotEmptyRule<T> : BusinessRuleBase<T> where T : class
    {
        public NotEmptyRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes)
            : base(errorMessage, states, attributes) { }

        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                object value = string.Empty;

                if (!HasState(state))
                    return true;

                foreach (var attribute in Attributes)
                {
                    value = attribute(inputObject);

                    if (value == null || value.GetType() == Type.GetType("System.String") && string.IsNullOrEmpty(value.ToString()))
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
