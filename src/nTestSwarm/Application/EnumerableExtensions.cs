using System;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action != null)
                foreach (var item in items)
                    action(item);
        }

        public static IEnumerable<T> Yield<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                if (action != null)
                    action(item);
                yield return item;
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }
    }

    public static class EnumExtensions
    {
        public static T Parse<T>(this string key) where T : struct
        {
            return (T) Enum.Parse(typeof (T), key);
        }
    }
}