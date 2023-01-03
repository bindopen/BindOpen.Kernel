namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoItems
    {
        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q NewSet<Q, T>(params T[] items)
            where Q : TBdoItemSet<T>, new()
            where T : IReferenced
        {
            return (new Q()).WithItems(items) as Q;
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoItemSet<T> NewSet<T>(params T[] items)
            where T : IReferenced
        {
            return NewSet<TBdoItemSet<T>, T>(items);
        }
    }
}
