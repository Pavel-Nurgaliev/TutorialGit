using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsImplementations
{
    public static class Helper
    {
        //where T : IComparable<T> constraits that our generic type has an implementatiuon if IComparable<T> interface
        public static T Max<T>(IEnumerable<T> items) where T : IComparable<T>
        {
            if (items is null)
            {
                throw new ArgumentNullException($"Items {nameof(items)} are null");
            }

            T max = default!;
            bool hasValue = false;

            foreach (var item in items)
            {
                if (!hasValue || item.CompareTo(max) > 0)
                {
                    max = item;
                    hasValue = true;
                }
            }

            if (!hasValue)
            {
                throw new InvalidOperationException("Sequence is empty");
            }

            return max;
        }
    }
}
