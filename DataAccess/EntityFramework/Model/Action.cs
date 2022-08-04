using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class Action
    {
        public Action()
        {
            ServiceActions = new HashSet<ServiceAction>();
        }

        public byte ActionId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<ServiceAction> ServiceActions { get; set; }
    }
}
