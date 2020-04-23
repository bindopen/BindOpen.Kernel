using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a document item.
    /// </summary>
    [XmlType("Document", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "document", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class Document : NamedDataItem, IDocument
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Container of this instance. 
        /// </summary>
        [XmlElement("container")]
        public BdoCarrierConfiguration Container { get; set; } = null;

        /// <summary>
        /// Content of this instance. 
        /// </summary>
        [XmlElement("content")]
        public BdoEntityConfiguration Content { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        public Document() : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        public Document(string name = null)
            : this(null, null, name)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="container">The container to consider.</param>
        /// <param name="content">The content to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public Document(IBdoCarrierConfiguration container, IBdoEntityConfiguration content, string name = null)
            : base(name, "document_")
        {
            Container = container as BdoCarrierConfiguration;
            Content = content as BdoEntityConfiguration;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        // Update -----------------------------------------

        /// <summary>
        /// Updates this instance with the specified document item.
        /// </summary>
        /// <param name="documentDataItem">The document item to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <returns>The log of the schema update.</returns>
        public virtual IBdoLog Update(IDocument documentDataItem, string relativePath = "")
        {
            return new BdoLog();
        }

        // Detection -----------------------------------------

        /// <summary>
        /// Detects the format of this instance considering the specified data source.
        /// </summary>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="log">The log to consider.</param>
        public virtual IBdoFormatConfiguration DetectFormat(
            IDatasource dataSource,
            ref IBdoLog log)
        {
            return new BdoFormatConfiguration();
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            Document dataEntityItem = base.Clone() as Document;
            if (this.Container != null)
                dataEntityItem.Container = this.Container.Clone() as BdoCarrierConfiguration;
            if (this.Content != null)
                dataEntityItem.Content = this.Content.Clone() as BdoEntityConfiguration;

            return dataEntityItem;
        }

        #endregion
    }
}