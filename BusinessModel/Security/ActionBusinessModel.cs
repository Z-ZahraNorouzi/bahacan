using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class ActionBusinessModel : BusinessModelBase
    {

        public byte? ActionId { get; set; }
        public string Title { get; set; } = null!;

    }
}
