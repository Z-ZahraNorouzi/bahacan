﻿
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessModelRules
{
    public class UserPermissionBusinessRule : Decorator<UserPermissionBusinessModel>
    {
        public UserPermissionBusinessRule(UserPermissionBusinessModel input)
            : base(input)
        {

        }

        public override void AddBusinessRules()
        {

        }
    }
}