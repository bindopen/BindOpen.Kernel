﻿using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data
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
        public static BdoStringSet NewStringSet()
        {
            return new BdoStringSet();
        }

        /// <summary>
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoStringSet NewStringSet(
            IEnumerable<string> addedValues = null,
            IEnumerable<string> removedValues = null)
        {
            var filter = NewStringSet()
                .Adding(addedValues?.ToArray())
                .Removing(removedValues?.ToArray());
            return filter;
        }
    }
}
