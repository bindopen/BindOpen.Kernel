using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{

    /// <summary>
    /// This class represents a carrier element.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "carrier", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierElement : DataElement
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private String _DefinitionUniqueName = "";

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition name of this instance.
        /// </summary>
        [XmlElement("definition")]
        public String DefinitionUniqueName
        {
            get { return this._DefinitionUniqueName; }
            set { this._DefinitionUniqueName = value; }
        }

        // --------------------------------------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new CarrierElementSpecification Specification
        {
            get { return base.Specification as CarrierElementSpecification; }
            set { base.Specification = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        public CarrierElement()
            : base(null, "carrierElement_")
        {
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="aCarrierUniqueName ">The carrier unique name to consider.</param>
        /// <param name="aSpecification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public CarrierElement(
            String name,
            String id,
            String aCarrierUniqueName,
            CarrierElementSpecification aSpecification,
            params CarrierConfiguration[] items)
            : base(name, "CarrierElement_", id)
        {
            this.ValueType = DataValueType.CarrierConfiguration;
            //this.Specification = new CarrierElementSpecification();

            this.SetItem(items);
            if (!String.IsNullOrEmpty(aCarrierUniqueName))
                this._DefinitionUniqueName = aCarrierUniqueName;
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="aCarrierUniqueName ">The carrier unique name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public CarrierElement(
            String name,
            String aCarrierUniqueName,
            params CarrierConfiguration[] items)
            : this(name, null, aCarrierUniqueName, null, items)
        {
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public CarrierElement(
            String name,
            params CarrierConfiguration[] items)
            : this(name, null, null, null, items)
        {
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public CarrierElement(
            params CarrierConfiguration[] items)
            : this(null, null, null, null, items)
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
            return new CarrierElementSpecification();
        }

        #endregion


        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="item">The item to apply to this instance.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public override void SetItem(
            Object item,
            AppScope appScope = null)
        {
            base.SetItem(item);
            
            if (this[0] is CarrierConfiguration)
                this._DefinitionUniqueName = (this[0] as CarrierConfiguration).DefinitionUniqueId;
        }

        /// <summary>
        /// Gets a new item of this instance.
        /// </summary>
          /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns a new object of this instance.</returns>
        public override Object NewItem(IAppScope appScope = null, Log log = null)
        {
            if (appScope == null 
                || appScope.AppExtension== null 
                || !(this.Specification is CarrierElementSpecification))
                return null;

            return appScope.AppExtension.CreateConfiguration<CarrierDefinition>(this.DefinitionUniqueName, null, log);
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
                   .Any(p => p is CarrierConfiguration && string.Equals((p as CarrierConfiguration)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        /// <summary>
        /// Indicates whether this instance contains the specified scalar item or the specified entity name.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="isCaseSensitive">Indicates whether the verification is case sensitive.</param>
        /// <returns>Returns true if this instance contains the specified scalar item or the specified entity name.</returns>
        public override Boolean HasItem(Object indexItem, Boolean isCaseSensitive = false)
        {
            if (indexItem is String)
                return this.Items.Any(p => p.KeyEquals(indexItem));

            return false;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return String.Join("|", this.Items.Select(p => (p as EntityConfiguration)?.Key() ?? "").ToArray());
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override String GetStringFromObject(
            Object object1,
            Log log = null)
        {
            String stringValue = "";

            if (object1 is CarrierConfiguration)
            {
                CarrierConfiguration dataCarrier = object1 as CarrierConfiguration;
                if (dataCarrier != null)
                    stringValue = dataCarrier.ToXml();
                else if (log!=null)
                    log.AddError(title: "Entity expected", description: "The specified object is not an entity.");
            }

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override Object GetObjectFromString(
            String stringValue,
            IAppScope appScope = null,
            Log log = null)
        {
            Object object1 = null;

            if (stringValue != null)
                if (this.Specification != null && appScope!= null && appScope.AppExtension != null)
                    object1 = appScope.AppExtension.CreateConfiguration<CarrierDefinition>(this.DefinitionUniqueName, stringValue, log);

            return object1;
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
            CarrierElement dataCarrierElement = this.MemberwiseClone() as CarrierElement;
            return dataCarrierElement;
        }

        #endregion

    }

}

