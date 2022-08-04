using DataAccess;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FactoryContainer
    {

        public IDaoFactories  Factory { get; set; }

        public FactoryContainer(IDaoFactories factory)
        {
            Factory = factory;
        }

        public FactoryContainer()
        {
            Factory = DataAccess.DataSource.Factory;
        }

    }
}
