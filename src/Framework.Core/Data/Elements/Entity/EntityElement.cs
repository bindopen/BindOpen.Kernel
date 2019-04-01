using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Entity
{

    /// <summary>
    /// This class represents a entity element that is an element whose items are entities.
    /// </summary>
    [Serializable()]
    [XmlType("EntityElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityElement : DataElement, IEntityElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Entity -----------------------------

        /// <summary>
        /// The entity unique name of this instance.
        /// </summary>
        [XmlAttribute("entity")]
        public string EntityUniqueName { get; set; } = "";

        // Specifcation -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new EntityElementSpec Specification
        {
            get { return base.Specification as EntityElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new entity element.
        /// </summary>
        public EntityElement()
            : base(null, "entityElement_")
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="entityUniqueName">The entity unique name to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public EntityElement(
            string name,
            string id,
            string entityUniqueName,
            IEntityElementSpec specification,
            params IDataItem[] items)
            : base(name, "EntityElement_", id)
        {
            this.ValueType = DataValueType.Entity;
            this.Specification = specification;

            this.SetItem(items);
            if (!string.IsNullOrEmpty(entityUniqueName))
                this.EntityUniqueName = entityUniqueName;
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="entityUniqueName">The entity unique name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public EntityElement(
            string name,
            string entityUniqueName,
            params DataItem[] items)
            : this(name, null, entityUniqueName, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public EntityElement(
            string name,
            params DataItem[] items)
            : this(name, null, null, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public EntityElement(
            params DataItem[] items)
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
        public override DataElementSpec CreateSpecification()
        {
            return new EntityElementSpec();
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
            object item,
            IAppScope appScope = null)
        {
            base.SetItem(item);
        }

        /// <summary>
        /// Gets a new item of this instance.
        /// </summary>
          /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns a new object of this instance.</returns>
        public override object NewItem(IAppScope appScope = null, ILog log = null)
        {
            if (this.Specification==null
                || (this.Specification is EntityElementSpec) && (this.Specification as EntityElementSpec).EntityFilter.IsValueAllowed(this.EntityUniqueName))

                if (appScope?.AppExtension != null)
                    return appScope.CreateItem<EntityDefinition>(this.EntityUniqueName, null, null, log);

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
        public override object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            else if (indexItem is string)
                return this.GetItems(appScope, scriptVariableSet, log)
                    .Any(p => p is NamedDataItem && string.Equals((p as NamedDataItem)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));

            return null;
        }

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
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("|", this.Items.Select(p => !(p is NamedDataItem) ? "" : (p as NamedDataItem).Name.ToString()).ToArray());
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            Object object1,
            ILog log = null)
        {
            string stringValue = "";

            if (object1 is EntityConfiguration)
            {
                EntityConfiguration item = object1 as EntityConfiguration;
                if (item != null)
                    stringValue = item.ToXml();
                else if (log !=null)
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
        public override object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            Object object1 = null;

            if (stringValue != null)
                if ((this.Specification == null || this.Specification is EntityElementSpec)
                    && (appScope != null && appScope.AppExtension!= null))
                    appScope.AppExtension.LoadDataItemInstance(AssemblyHelper.GetClassReference(this.EntityUniqueName), stringValue, out object1);

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
        public override object Clone()
        {
            EntityElement aEntityElement = this.MemberwiseClone() as EntityElement;
            //if (this.DataSchemreference != null)
            //    aEntityElement.DataSchemreference = this.DataSchemreference.Clone() as DataHandler;

            return aEntityElement;
        }

        #endregion
    }
}
