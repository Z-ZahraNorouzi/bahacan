using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class UserServiceItemBusinessModel : BusinessModelBase
    {
        public long? UserServiceItemId { get; set; }
        public long UserId { get; set; }
        public long ServiceItemId { get; set; }
        public DateTime CreationDate { get; set; }
        public long CreateById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? DeletedById { get; set; }

        public virtual ServiceItemBusinessModel? ServiceItem { get; set; } = null!;
    }
}
