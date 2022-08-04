
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessModelRules
{
    public class LoginHistoryBusinessRule : Decorator<LoginHistoryBusinessModel>
    {
        public LoginHistoryBusinessRule(LoginHistoryBusinessModel input)
            : base(input)
        {

        }

        public override void AddBusinessRules()
        {

        }
    }
}