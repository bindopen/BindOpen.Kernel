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
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q NewList<Q, T>(params T[] items)
            where Q : class, ITBdoList<T>, new()
            where T : IReferenced
        {
            return (new Q()).With(items) as Q;
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static TBdoList<T> NewList<T>(params T[] items)
            where T : IReferenced
        {
            return NewList<TBdoList<T>, T>(items);
        }
    }
}
