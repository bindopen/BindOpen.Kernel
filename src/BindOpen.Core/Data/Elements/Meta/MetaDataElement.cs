using BindOpen.Data.Helpers.Objects;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a meta data element that is a data element whose items are data elements.
    /// </summary>
    [Serializable()]
    [XmlType("MetaDataElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "meta", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [XmlInclude(typeof(DocumentElement))]
    public class MetaDataElement : DataElement, IMetaDataElement
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new meta data element.
        /// </summary>
        public MetaDataElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new meta data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        public MetaDataElement(string name = null, string namePreffix = null)
            : base(name, namePreffix)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override bool HasItem(object indexItem, bool isCaseSensitive = false)
        {
            if (indexItem is string)
                return this.Items.Any(p => p.KeyEquals(indexItem));

            return false;
        }

        /// <summary>
        /// Creates a new specification of this instance.
        /// </summary>
        /// <returns>Returns True .</returns>
        public override DataElementSpec NewSpecification()
        {
            return null;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", this.Items.Select(p => !(p is DataElement) ? "" : (p as DataElement).Name.ToString()).ToArray());
        }

        #endregion
    }
}
