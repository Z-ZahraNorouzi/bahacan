using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{

    public class LogicalRule<T> : BusinessRuleBase<T> where T : class
    {
        private Func<object, object, bool> ValidationOperator;
        public LogicalRule(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes, Func<object, object, bool> validationOperator)
            : base(errorMessage, states, attributes)
        {
            ValidationOperator = validationOperator;
        }

        public override bool Validate(T inputObject, BusinessObjectState state)
        {
            try
            {
                if (!HasState(state))
                    return true;

                if (Attributes.Length != 2)
                    return true;

                return ValidationOperator(Attributes[0](inputObject), Attributes[1](inputObject));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
