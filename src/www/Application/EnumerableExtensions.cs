using nTestSwarm.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace nTestSwarm.Application
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action != null)
                foreach (var item in items)
                    action(item);

            return items;
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

        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return !IsEmpty(items);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static IEnumerable<Descriptor> ToDescriptors<TItem>(this IEnumerable<TItem> items) where TItem : INamedEntity
        {
            return items.Select(i => new Descriptor { Id = i.Id, Name = i.Name });
        }

        public static IEnumerable<Descriptor> SelectedBy(this IEnumerable<Descriptor> items, IEnumerable<long> ids)
        {
            if (ids.IsEmpty())
                return items;

            return SelectedBy(items, d => ids.Contains(d.Id));
        }


        public static IEnumerable<Descriptor> SelectedBy(this IEnumerable<Descriptor> items, Func<Descriptor,bool> predicate)
        {
            if (items.IsNotEmpty())
                items.Where(predicate)
                     .Each(ua => ua.Selected = true);

            return items;
        }

        public static IEnumerable<SelectListItem> SelectedBy(this IEnumerable<SelectListItem> items, IEnumerable<long> ids)
        {
            if (ids.IsEmpty())
                return items;

            return SelectedBy(items, d => ids.Contains(int.Parse(d.Value)));
        }

        public static IEnumerable<SelectListItem> SelectedBy(this IEnumerable<SelectListItem> items, Func<SelectListItem, bool> predicate)
        {
            if (items.IsNotEmpty())
                items.Where(predicate)
                     .Each(ua => ua.Selected = true);

            return items;
        }

        public static IEnumerable<Descriptor> ClearSelections(this IEnumerable<Descriptor> items)
        {
            if (items.IsNotEmpty())
                return items.Each(i => i.Selected = false);
            else 
                return items;
        }

        public static IEnumerable<SelectListItem> ClearSelections(this IEnumerable<SelectListItem> items)
        {
            if (items.IsNotEmpty())
                return items.Each(i => i.Selected = false);
            else
                return items;
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