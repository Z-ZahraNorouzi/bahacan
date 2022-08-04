using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum FilterOperation
    {
        GreaterOrEqual = 1,
        Greater = 2,
        LessOrEqual = 3,
        Less = 4,
        Equal = 5,
        NotEqual = 6,
        Contains = 7,
        NotContains = 8,
        StartsWith = 9,
        EndsWith = 10
    }
}
