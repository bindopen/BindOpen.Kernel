using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents indexed data item.
    /// </summary>
    [Serializable()]
    [XmlType("IndexedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("indexedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class IndexedDataItem : DescribedDataItem, IIndexedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [XmlElement("index")]
        public int Index { get; set; } = -1;

        /// <summary>
        /// Specification of the Index property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool IndexSpecified => Index >= 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the IndexedDataItem class.
        /// </summary>
        public IndexedDataItem() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the IndexedDataItem class considering the specified preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        public IndexedDataItem(string name,
            string namePreffix = "",
            string id = null)
            : base(name, namePreffix, id)
        {
        }

        #endregion
    }
}
