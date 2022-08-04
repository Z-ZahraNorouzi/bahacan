using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class TenantBusinessModel : BusinessModelBase
    {

        public long? TenantId { get; set; }
        public string? Title { get; set; }
        public long? CompanyId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreateById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? DeletedById { get; set; }
    }
}
