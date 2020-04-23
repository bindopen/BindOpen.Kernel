using System.Linq;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class ItemFactory
    {
        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q CreateSet<Q, T>(params T[] items)
            where Q : DataItemSet<T>, new()
            where T : IIdentifiedDataItem
        {
            return new Q()
            {
                Items = items?.ToList()
            };
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static DataItemSet<T> CreateItemSet<T>(params T[] items) where T : IIdentifiedDataItem
        {
            return CreateSet<DataItemSet<T>, T>(items);
        }
    }
}
