using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
    {
        private static Random rng = new Random();

        /// <summary>
        /// (extension) Randomizes a List
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IList<T> Paging<T>(this IList<T> list, int page, int pageSize = 50)
        {
            if (list.IsNullOrEmpty())
            {
                return null;
            }
            int n = list.Count;
            if (page == 1)
            {
                return list.Take(pageSize).ToList();
            }
            else
            {
                int i = 0;
                if ((page - 1) * pageSize > list.Count())
                {
                    return null;
                }

                while (i < (page - 1) * pageSize)
                {
                    list.RemoveAt(0);
                    i++;
                }
                return list.Take(pageSize).ToList();
            }
        }

        public static bool IsNullOrEmpty<T>(this IQueryable<T> list)
        {
            if (list.IsNull() || list.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list.IsNull() || list.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<TItem> DistinctBy<TItem, TValue>(this IEnumerable<TItem> items, Func<TItem, TValue> selector)
        {
            var uniques = new HashSet<TValue>();
            foreach (var item in items)
            {
                if (uniques.Add(selector(item))) yield return item;
            }
        }
    }
}
