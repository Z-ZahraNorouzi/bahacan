using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class ServiceActionBusinessModel : BusinessModelBase
    {
        public long? ServiceActionId { get; set; }
        public byte ActionId { get; set; }
        public int ServiceId { get; set; }
        public string? Title { get; set; }
        public string? ApiController { get; set; }
        public string? ApiService { get; set; }
        public string? ControllerRoute { get; set; }
        public string? ServiceRoute { get; set; }

        public virtual ActionBusinessModel? Action { get; set; } = null!;
    }
}
