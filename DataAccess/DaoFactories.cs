using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DaoFactories
    {
        public static IDaoFactories GetFactory(string dataAccessMethod, string dataAccessProvider)
        {
            switch (dataAccessMethod.ToLower())
            {
                //case "adonet":
                //    switch (dataAccessProvider.ToLower())
                //    {
                //        case "ms-sql":
                //            return new DataObjects.AdoNet.MS_SQL.DaoFactory();
                //        //case "oracle":
                //        //    return new DataObjects.AdoNet.Oracle.DaoFactory();
                //        default:
                //            return new DataObjects.AdoNet.MS_SQL.DaoFactory();
                //    }

                case "entityframework":
                    switch (dataAccessProvider.ToLower())
                    {
                        case "ms-sql":
                            return new DataAccess.EntityFramework.MS_SQL.DaoFactory();
                        //case "oracle":
                        //    return new DataObjects.EntityFrameWork.Oracle.DaoFactory();
                        default:
                            return new DataAccess.EntityFramework.MS_SQL.DaoFactory();
                    }

                default:
                    return new DataAccess.EntityFramework.MS_SQL.DaoFactory();
            }

        }
    }
}
