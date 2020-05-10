using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents indexed data item.
    /// </summary>
    [XmlType("IndexedDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("indexedDataItem", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        [DefaultValue(-1)]
        public int Index { get; set; } = -1;

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

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IIndexedDataItem WithIndex(int index)
        {
            Index = index;
            return this;
        }

        #endregion
    }
}
