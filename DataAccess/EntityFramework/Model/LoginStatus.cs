using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class LoginStatus
    {
        public LoginStatus()
        {
            LoginHistories = new HashSet<LoginHistory>();
        }

        public int LoginStatusId { get; set; }
        public string TitleFa { get; set; } = null!;
        public string? TitleEn { get; set; }

        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
    }
}
