using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticData
{
    public static class UserInfo
    {
        public static UserInfoBusinessModel CurrentUser { get; set; }
    }
}
