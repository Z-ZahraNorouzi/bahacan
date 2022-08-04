using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum BuyRequestStatus
    {
        /// <summary>
        /// درخواست ثبت شده
        /// </summary>
        Accepted = 1,
        /// <summary>
        /// کارتابل مدیر
        /// </summary>
        Manager = 2,
        /// <summary>
        /// کارتابل انبار
        /// </summary>
        Warehouse = 3,
        /// <summary>
        /// در دست تامین
        /// </summary>
        UnderSupply = 4,
        /// <summary>
        /// درخواست به اتمام رسیده
        /// </summary>
        Finishied = 5
    }

    public enum ManagerApproveStatus
    {
        /// <summary>
        /// ثبت شده
        /// </summary>
        Registered = 0,
        /// <summary>
        /// تایید
        /// </summary>
        Accepted=1,
        /// <summary>
        /// رد
        /// </summary>
        Rejected=2

    }
}

