using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Exceptions
{
    public class ErrorDescriber
    {
        public string Describe(string key)
        {
            var manager = new ResourceManager(typeof(Resources.Resource));
            var parts = key.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            var path = new List<string>();
            foreach (var part in parts)
            {
                string pathPart = manager.GetString(part);
                if (!string.IsNullOrEmpty(pathPart))
                    path.Add(pathPart);
            }

            return string.Join(" => ", path.ToArray());
        }

        public string Conflict(string tableName, string fieldName)
        {
            return Describe(tableName + "_" + fieldName);
        }
    }
}
