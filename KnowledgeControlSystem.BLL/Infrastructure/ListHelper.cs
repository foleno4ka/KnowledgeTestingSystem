using System.Collections.Generic;
using System.Linq;

namespace KnowledgeControlSystem.BLL.Infrastructure
{
    public class ListHelper
    {
        public static bool ContainsAll<T>(IEnumerable<T> source, IEnumerable<T> values)
        {
            return values.All(source.Contains);
        }
    }
}