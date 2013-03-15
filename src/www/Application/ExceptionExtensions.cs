using System;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application
{
    //http://stackoverflow.com/questions/9314172/getting-all-messages-from-innerexceptions
    public static class ExceptionExtensions
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (TSource current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }


        public static string GetAllMessages(this Exception exception)
        {
            IEnumerable<string> messages = exception.FromHierarchy(ex => ex.InnerException)
                                                    .Select(ex => ex.ToString());
            return String.Join(Environment.NewLine, messages);
        }
    }
}