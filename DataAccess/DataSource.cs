using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataSource
    {
        public static IDaoFactories Factory
        {
            get
            {
                string _method = "entityframework";
                string _provider = "ms-sql";
                return DaoFactories.GetFactory(_method, _provider);
            }
        }


    }
}
