using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Document
{

    /// <summary>
    /// This class represents a document element that is an element whose items are documents.
    /// </summary>
    [Serializable()]
    [XmlType("DocumentElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "document", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DocumentElement : DataElement
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
            get { return this.GetItem(0) as CarrierElement; }
        }

        /// <summary>
        /// The content of this instance.
        /// </summary>
        [XmlElement("content")]
        public EntityElement ContentElement
        {
            get { return this.GetItem(1) as EntityElement; }
        }

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new DocumentElementSpecification Specification
        {
            get { return base.Specification as DocumentElementSpecification; }
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
        public DocumentElement() : this(null, null as CarrierConfiguration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="aContainerElement">The container element to consider.</param>
        /// <param name="aContentElement">The content element to consider.</param>
        public DocumentElement(
            String name = null, 
            CarrierElement aContainerElement = null,
            EntityElement aContentElement = null)
            : base(name, "documentElement_")
        {
            this.ValueType = DataValueType.Document;
            this.ClearItems();
            this.AddItem(aContainerElement ?? new CarrierElement());
            this.AddItem(aContentElement ?? new EntityElement());
        }

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        /// <param name="aContainerElement">The container element to consider.</param>
        /// <param name="aContentElement">The content element to consider.</param>
        public DocumentElement(
            CarrierElement aContainerElement = null,
            EntityElement aContentElement = null)
            : this(null, aContainerElement, aContentElement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="aContainerItem">The container item to consider.</param>
        /// <param name="aContentItem">The content item to consider.</param>
        public DocumentElement(
            String name = null,
            CarrierConfiguration aContainerItem = null,
            EntityConfiguration aContentItem = null)
            : this(name, new CarrierElement(aContainerItem), new EntityElement(aContentItem))
        {
        }

        /// <summary>
        /// Initializes a new instance of the DocumentElement class.
        /// </summary>
        /// <param name="aContainerItem">The container item to consider.</param>
        /// <param name="aContentItem">The content item to consider.</param>
        public DocumentElement(
            CarrierConfiguration aContainerItem = null,
            EntityConfiguration aContentItem = null)
            : this(null, aContainerItem, aContentItem)
        {
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override DataElementSpecification CreateSpecification()
        {
            return new DocumentElementSpecification();
        }

        #endregion


        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Gets a new item of this instance.
        /// </summary>
          /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns a new object of this instance.</returns>
        public override Object NewItem(IAppScope appScope = null, Log log = null)
        {
            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override Object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if ((indexItem == null) || (indexItem is int))
            {
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            }
            else if (indexItem is String)
            {
                return this.GetItems(appScope, scriptVariableSet, log)
                    .Any(p => p is Items.Documents.Document && string.Equals((p as Items.Documents.Document)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The scalar item or the entity name to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override Boolean HasItem(Object indexItem, Boolean isCaseSensitive = false)
        {
            if (indexItem is String)
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
        public override Object Clone()
        {
            Items.Documents.Document aDocument = this.MemberwiseClone() as Items.Documents.Document;
            return aDocument;
        }

        #endregion

    }

}
