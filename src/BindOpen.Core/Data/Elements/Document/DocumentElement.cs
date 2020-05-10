using BindOpen.Data.Common;
using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a document element that is an element whose items are documents.
    /// </summary>
    [XmlType("DocumentElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "document", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
            get { return Items[0] as CarrierElement; }
        }

        /// <summary>
        /// The content of this instance.
        /// </summary>
        [XmlElement("content")]
        public ObjectElement ContentElement
        {
            get { return Items[1] as ObjectElement; }
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
            ValueType = DataValueTypes.Document;
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
        public override IDataElementSpec NewSpecification()
        {
            return Specification = new DocumentElementSpec();
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
        public override object Clone(params string[] areas)
        {
            Document document = base.Clone(areas) as Document;
            return document;
        }

        #endregion

    }
}
