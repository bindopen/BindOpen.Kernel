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
        public static TSet NewGroupsOf<TSet, TItem>(params TItem[] items)
            where TSet : ITBdoGroupsOf<TItem>, new()
            where TItem : IReferenced, IGrouped
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
        public static TBdoGroupsOf<TItem> NewGroupsOf<TItem>(params TItem[] items)
            where TItem : IReferenced, IGrouped
        {
            return NewGroupsOf<TBdoGroupsOf<TItem>, TItem>(items);
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static TSet ToBdoGroupsOf<TSet, TItem>(
            this IEnumerable<TItem> enumerable)
            where TSet : class, ITBdoGroupsOf<TItem>, new()
            where TItem : IReferenced, IGrouped
        {
            var set = NewGroupsOf<TSet, TItem>(enumerable?.ToArray());
            return set;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<TItem> ToBdoGroupsOf<TItem>(
            this IEnumerable<TItem> enumerable)
            where TItem : IReferenced, IGrouped
        {
            var set = NewGroupsOf(enumerable?.ToArray());
            return set;
        }
    }
}
