using BindOpen.Data.Items;
using System.Collections.Generic;

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
        public static BdoStringFilter NewStringFilter()
        {
            return new BdoStringFilter();
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static BdoStringFilter NewStringFilter(
            List<string> addedValues = null,
            List<string> removedValues = null)
        {
            var filter = NewStringFilter()
                .Adding(addedValues?.ToArray())
                .Removing(removedValues?.ToArray());
            return filter;
        }
    }
}
