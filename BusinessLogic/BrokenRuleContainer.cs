using BusinessLogic.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BrokenRuleContainer<T> : List<BusinessRuleBase<T>> where T : class
    {
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var item in this)
                result.AppendLine(item.ErrorMessage);

            return result.ToString();
        }
    }
}
