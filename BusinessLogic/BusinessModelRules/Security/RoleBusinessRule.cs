
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessModelRules
{
    public class RoleBusinessRule : Decorator<RoleBusinessModel>
    {
        public RoleBusinessRule(RoleBusinessModel input)
            : base(input)
        {

        }

        public override void AddBusinessRules()
        {

        }
    }
}
