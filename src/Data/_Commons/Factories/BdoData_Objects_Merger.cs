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
        public static BdoMerger NewMerger()
        {
            return new BdoMerger();
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoMerger NewMerger(
            IEnumerable<string> addedValues = null,
            IEnumerable<string> removedValues = null)
        {
            var filter = NewMerger()
                .Adding(addedValues?.ToArray())
                .Removing(removedValues?.ToArray());
            return filter;
        }
    }
}
