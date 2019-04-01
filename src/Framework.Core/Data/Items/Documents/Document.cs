using System;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Documents
{
    /// <summary>
    /// This class represents a document item.
    /// </summary>
    [Serializable()]
    [XmlType("Document", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "document", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
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
        public ICarrierConfiguration Container { get; set; } = null;

        /// <summary>
        /// Content of this instance. 
        /// </summary>
        [XmlElement("content")]
        public IEntityConfiguration Content { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the DataSource class.
        /// </summary>
        public Document() : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the DataSource class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        public Document(String name = null)
            : this(null, null, name)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the DataSource class.
        /// </summary>
        /// <param name="aContainer">The container to consider.</param>
        /// <param name="aContent">The content to consider.</param>
        /// <param name="name">The name of this instance.</param>
        public Document(ICarrierConfiguration aContainer, IEntityConfiguration aContent, String name = null)
            : base(name, "document_")
        {
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
        public virtual ILog Update(IDocument documentDataItem, String relativePath = "")
        {
            return new Log();
        }

        // Detection -----------------------------------------

        /// <summary>
        /// Detects the format of this instance considering the specified data source.
        /// </summary>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="log">The log to consider.</param>
        public virtual IFormatConfiguration DetectFormat(
            IDataSource dataSource,
            ref ILog log)
        {
            return new FormatConfiguration();
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
                dataEntityItem.Container = this.Container.Clone() as CarrierConfiguration;
            if (this.Content != null)
                dataEntityItem.Content = this.Content.Clone() as EntityConfiguration;

            return dataEntityItem;
        }

        #endregion
    }
}