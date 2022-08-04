using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessRules
{
    [Flags]
    public enum BusinessObjectState
    {
        Add = 1,
        Modify = 2,
        Remove = 4
    }
}
