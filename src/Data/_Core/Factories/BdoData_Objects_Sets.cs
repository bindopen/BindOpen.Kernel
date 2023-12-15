using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q NewItemSet<Q, T>(params T[] items)
            where Q : ITBdoSet<T>, new()
            where T : IReferenced
        {
            var q = new Q();
            q.With(items);
            return q;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<T> NewItemSet<T>(params T[] items)
            where T : IReferenced
        {
            return NewItemSet<TBdoSet<T>, T>(items);
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q ToBdoItemSet<Q, T>(
            this IEnumerable<T> enumerable)
            where Q : class, ITBdoSet<T>, new()
            where T : IReferenced
        {
            var set = NewItemSet<Q, T>(enumerable?.ToArray());
            return set;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<T> ToBdoItemSet<T>(
            this IEnumerable<T> enumerable)
            where T : IReferenced
        {
            var set = NewItemSet(enumerable?.ToArray());
            return set;
        }
    }
}
