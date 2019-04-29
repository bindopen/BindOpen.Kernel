using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Data.Elements.Document
{
    /// <summary>
    /// This class represents a document element that is an element whose items are documents.
    /// </summary>
    [Serializable()]
    [XmlType("DocumentElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "document", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DocumentElement : DataElement, IDocumentElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The container of this instance.
        /// </summary>
        [XmlElement("container")]
        public CarrierElement ContainerElement
        {
            get { return this.Items[0] as CarrierElement; }
        }

        /// <summary>
        /// The content of this instance.
        /// </summary>
        [XmlElement("content")]
        public ObjectElement ContentElement
        {
            get { return this.Items[1] as ObjectElement; }
        }

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new DocumentElementSpec Specification
        {
            get { return base.Specification as DocumentElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        public DocumentElement() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public DocumentElement(
            string name = null,
            string id = null)
            : base(name, "document_", id)
        {
            ValueType = DataValueType.Document;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override DataElementSpec NewSpecification()
        {
            return Specification = new DocumentElementSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The scalar item or the entity name to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override bool HasItem(object indexItem, bool isCaseSensitive = false)
        {
            if (indexItem is string)
                return this.Items.Any(p => p is Items.Documents.Document && string.Equals((p as Items.Documents.Document)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));

            return false;
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair


        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            Items.Documents.Document aDocument = this.MemberwiseClone() as Items.Documents.Document;
            return aDocument;
        }

        #endregion

    }
}
