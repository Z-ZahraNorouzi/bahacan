using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class LoginStatusBusinessModel : BusinessModelBase
    {
        public int? LoginStatusId { get; set; }
        public string TitleFa { get; set; } = null!;
        public string? TitleEn { get; set; }

    }
}
