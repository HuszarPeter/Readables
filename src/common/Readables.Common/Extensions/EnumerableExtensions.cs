using System;
using System.Collections;
using System.Collections.Generic;

namespace Readables.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach(var item in enumerable)
            {
                action(item);
            }
        }

		public static void CallOn<T>(this object target, Action<T> action) where T : class
		{
            if (target is T subject)
            {
                action(subject);
            }
        }

		public static void CallOnEach<T>(this IEnumerable enumerable, Action<T> action) where T : class
		{
			foreach (object o in enumerable)
			{
				o.CallOn(action);
			}
		}
    }
}
