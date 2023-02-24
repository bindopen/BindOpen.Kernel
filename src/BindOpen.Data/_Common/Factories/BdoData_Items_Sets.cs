using BindOpen.Data.Items;

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
        public static Q NewSet<Q, T>(params T[] items)
            where Q : class, ITBdoSet<T>, new()
            where T : IReferenced
        {
            return (new Q()).With(items) as Q;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoSet<T> NewSet<T>(params T[] items)
            where T : IReferenced
        {
            return NewSet<TBdoSet<T>, T>(items);
        }
    }
}
