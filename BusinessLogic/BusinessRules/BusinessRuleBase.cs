using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    public abstract class BusinessRuleBase<T> where T : class
    {
        public Func<T, object>[] Attributes { get; private set; }

        public string ErrorMessage { get; private set; }

        public BusinessObjectState States { get; private set; }

        public BusinessRuleBase(string errorMessage, BusinessObjectState states, Func<T, object>[] attributes)
        {
            Attributes = attributes;
            States = states;
            ErrorMessage = errorMessage;
        }

        public bool HasState(BusinessObjectState state)
        {
            return (States & state) != 0;
        }

        public abstract bool Validate(T inputObject, BusinessObjectState state);
    }
}
