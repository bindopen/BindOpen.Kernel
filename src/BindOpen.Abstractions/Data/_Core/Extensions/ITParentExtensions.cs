using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {
        public static IEnumerable<T> Last<T>(
            this ITParent<T> obj,
            T parent = default)
            where T : IReferenced
        {
            var list = new List<T>();

            if (obj != null)
            {
                var child = obj._Children == null ? default : obj._Children.LastOrDefault();

                if (child is ITParent<T> childParent)
                {
                    foreach (var subChild in childParent._Children)
                    {
                        list.AddRange(childParent.Last(subChild));
                    }
                }
                else
                {
                    list.Add(child);
                }
            }

            return list;
        }
    }
}
