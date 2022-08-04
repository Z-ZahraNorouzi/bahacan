
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessModelRules
{
    public class ServiceBusinessRule : Decorator<ServiceBusinessModel>
    {
        public ServiceBusinessRule(ServiceBusinessModel input)
            : base(input)
        {

        }

        public override void AddBusinessRules()
        {

        }
    }
}