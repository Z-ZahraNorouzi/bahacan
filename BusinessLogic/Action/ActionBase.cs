using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Action
{
    public abstract class ActionBase
    {
        private FactoryContainer _factoryContainer;

        public FactoryContainer FactoryContainer
        {
            get
            {
                if (_factoryContainer == null)
                    _factoryContainer = new FactoryContainer();
                return _factoryContainer;
            }
            set
            {
                _factoryContainer = value;
            }
        }

        public IEnumerable<T> Minus<T, K>(IEnumerable<T> source1, IEnumerable<K> source2, Func<T, K, bool> compare)
        {
            List<T> result = new List<T>();

            if (source1 == null)
                return result;
            if (source2 == null)
                return source1;

            foreach (var itemSource1 in source1)
            {
                if (source2.Where(x => compare.Invoke(itemSource1, x)).Count() == 0)
                    result.Add(itemSource1);
            }

            return result;
        }

        public IEnumerable<T> Intersect<T, K>(IEnumerable<T> source1, IEnumerable<K> source2, Func<T, K, bool> compare)
        {
            List<T> result = new List<T>();

            if (source1 == null || source2 == null)
                return result;

            foreach (var itemSource1 in source1)
            {
                if (source2.Where(x => compare.Invoke(itemSource1, x)).Count() > 0)
                    result.Add(itemSource1);
            }

            return result;
        }


    }
}
