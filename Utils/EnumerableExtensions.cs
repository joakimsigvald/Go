using System;
using System.Collections.Generic;
using System.Linq;

namespace Go.Utilities
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            list.ToList().ForEach(action);
        }
    }
}
