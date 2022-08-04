using System;
using System.Collections.Generic;

namespace DataAccess.EntityFramework.Model
{
    public partial class Service
    {
        public Service()
        {
            InverseParent = new HashSet<Service>();
            ServiceActions = new HashSet<ServiceAction>();
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int ServiceId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Controller { get; set; }
        public string? Icon { get; set; }
        public bool IsService { get; set; }
        public int ItemOrder { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Service? Parent { get; set; }
        public virtual ICollection<Service> InverseParent { get; set; }
        public virtual ICollection<ServiceAction> ServiceActions { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
