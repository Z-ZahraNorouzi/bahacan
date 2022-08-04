using BusinessLogic.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public abstract class Decorator<T> where T : class
    {
        private List<BusinessRuleBase<T>> _rules;

        private BrokenRuleContainer<T> _brokenRules;

        private T _businessObject;

        public List<BusinessRuleBase<T>> Rules
        {
            get
            {
                if (_rules == null)
                {
                    _rules = new List<BusinessRuleBase<T>>();
                    AddBusinessRules();
                }
                return _rules;
            }
        }

        public BrokenRuleContainer<T> BrokenRules
        {
            get
            {
                if (_brokenRules == null)
                    _brokenRules = new BrokenRuleContainer<T>();

                return _brokenRules;
            }
        }

        public Decorator(T inputObject)
        {
            _businessObject = inputObject;
        }


        public bool Validate(BusinessObjectState states)
        {
            BrokenRules.Clear();

            foreach (var rule in Rules)
            {
                if (!rule.Validate(_businessObject, states))
                    BrokenRules.Add(rule);

            }

            return BrokenRules.Count == 0;
        }

        public abstract void AddBusinessRules();
    }
}
