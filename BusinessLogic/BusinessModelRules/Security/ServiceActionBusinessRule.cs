
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessModelRules
{
    public class ServiceActionBusinessRule : Decorator<ServiceActionBusinessModel>
    {
        public ServiceActionBusinessRule(ServiceActionBusinessModel input)
            : base(input)
        {

        }

        public override void AddBusinessRules()
        {

        }
    }
}
