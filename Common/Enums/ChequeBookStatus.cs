using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum ChequeBookStatus
    {
        /// <summary>
        /// موجود
        /// </summary>
        Available = 1,
        /// <summary>
        /// اتمام برگه های چک
        /// </summary>
        Finished = 2,
        /// <summary>
        /// مفقود شده
        /// </summary>
        Lost = 3,
        /// <summary>
        /// مسدود شده
        /// </summary>
        Blocked = 4
    }

}
