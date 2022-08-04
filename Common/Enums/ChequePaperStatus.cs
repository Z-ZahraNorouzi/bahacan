using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum ChequePaperStatus
    {
        /// <summary>
        /// موجود
        /// </summary>
        Available = 1,
        /// <summary>
        /// صادر شده
        /// </summary>
        Issued = 2,
        /// <summary>
        /// باطل شد
        /// </summary>
        Expired = 3
    }

}
