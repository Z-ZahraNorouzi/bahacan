using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class ServiceBusinessModel : BusinessModelBase
    {
        public int? ServiceId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Controller { get; set; }
        public string? Icon { get; set; }
        public bool IsService { get; set; }
        public int ItemOrder { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ServiceBusinessModel? Parent { get; set; }
        public virtual ICollection<ServiceActionBusinessModel>? ServiceActions { get; set; }
        public virtual ICollection<ServiceItemBusinessModel>? ServiceItems { get; set; }
    }
}
