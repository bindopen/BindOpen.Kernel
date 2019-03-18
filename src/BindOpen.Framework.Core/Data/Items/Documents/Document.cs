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
    public class Document : NamedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private CarrierConfiguration _Container = null;
        private EntityConfiguration _Content = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Container of this instance. 
        /// </summary>
        [XmlElement("container")]
        public CarrierConfiguration Container
        {
            get
            {
                return this._Container;
            }
            set
            {
                this._Container = value;
            }
        }

        /// <summary>
        /// Content of this instance. 
        /// </summary>
        [XmlElement("content")]
        public EntityConfiguration Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

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
        public Document(CarrierConfiguration aContainer, EntityConfiguration aContent, String name = null)
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
        /// <param name="aDocumentDataItem">The document item to consider.</param>
        /// <param name="relativePath">The relative path to consider.</param>
        /// <returns>The log of the schema update.</returns>
        public virtual Log Update(Document aDocumentDataItem, String relativePath = "")
        {
            return new Log();
        }
        
        // Detection -----------------------------------------

        /// <summary>
        /// Detects the format of this instance considering the specified data source.
        /// </summary>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="log">The log to consider.</param>
        public virtual FormatConfiguration DetectFormat(
            DataSource dataSource,
            ref Log log)
        {
            return new FormatConfiguration();
        }

        #endregion

        //// ------------------------------------------
        //// LOAD
        //// ------------------------------------------

        //#region Load

        ///// <summary>
        ///// Instantiates a new instance of EntityConfiguration class from a xml string considering the specified object type.
        ///// </summary>
        ///// <param name="xmlString">The Xml string to load.</param>
        ///// <param name="object1Type">The object type to consider.</param>
        //protected static DocumentDataItem LoadFromXmlString(String xmlString, Type object1Type)
        //{
        //    DocumentDataItem dataEntity = null;
        //    try
        //    {
        //        // we parse the xml string
        //        XDocument xDocument = XDocument.Parse(xmlString);

        //        // then we load
        //        XmlSerializer xmlSerializer = new XmlSerializer(object1Type);
        //        StringReader aStringReader = new StringReader(xmlString);
        //        dataEntity = (DocumentDataItem)xmlSerializer.Deserialize(XmlReader.Create(aStringReader));
        //    }
        //    catch
        //    {
        //    }

        //    return dataEntity;
        //}

        //#endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override Object Clone()
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