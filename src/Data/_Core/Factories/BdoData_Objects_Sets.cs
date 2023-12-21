using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
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
        public static TSet NewItemSet<TSet, TItem>(params TItem[] items)
            where TSet : ITBdoSet<TItem>, new()
            where TItem : IReferenced
        {
            var q = new TSet();
            q.With(items);
            return q;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<TItem> NewItemSet<TItem>(params TItem[] items)
            where TItem : IReferenced
        {
            return NewItemSet<TBdoSet<TItem>, TItem>(items);
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static TSet ToBdoItemSet<TSet, TItem>(
            this IEnumerable<TItem> enumerable)
            where TSet : class, ITBdoSet<TItem>, new()
            where TItem : IReferenced
        {
            var set = NewItemSet<TSet, TItem>(enumerable?.ToArray());
            return set;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<TItem> ToBdoItemSet<TItem>(
            this IEnumerable<TItem> enumerable)
            where TItem : IReferenced
        {
            var set = NewItemSet(enumerable?.ToArray());
            return set;
        }
    }
}
