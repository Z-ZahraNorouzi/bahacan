using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    /// <summary>
    /// استفاده به عنوان
    /// KeyValuePair
    /// </summary>
    public class KV<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KV() { }
        public KV(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
