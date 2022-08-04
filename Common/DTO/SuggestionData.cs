using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    /// <summary>
    /// استفاده برای دریافت پیشنهاد
    /// </summary>
    public class SuggestionData
    {
        public string Term { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        public List<KV<string, object>> Params { get; set; }

        public object GetParam(string key)
        {
            if (Params == null)
                return null;
            var param = Params.FirstOrDefault(x => string.Compare(x.Key, key, true) == 0);
            if (param == null)
                return null;
            return param.Value;
        }

        public void RemoveParam(string key)
        {
            if (Params == null)
                return;
            var param = Params.FirstOrDefault(x => string.Compare(x.Key, key, true) == 0);
            if (param == null)
                return;
            Params.Remove(param);
        }
    }
}
