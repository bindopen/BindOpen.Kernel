using System;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    [Serializable()]
    [XmlType("ObjectElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "object", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ObjectElement : DataElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // object -----------------------------

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        [XmlAttribute("class")]
        public string ClassFullName { get; set; } = "";

        // Specifcation -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new ObjectElementSpec Specification
        {
            get { return base.Specification as ObjectElementSpec; }
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
        public ObjectElement()
            : base(null, "ObjectElement_")
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="classFullName">The class full name to consider.</param>
        /// <param name="aSpecification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            string id,
            string classFullName,
            IObjectElementSpec aSpecification,
            params IDataItem[] items)
            : base(name, "ObjectElement_", id)
        {
            this.ValueType = DataValueType.Object;
            this.Specification = aSpecification;

            this.SetItem(items);
            if (!string.IsNullOrEmpty(classFullName))
                this.ClassFullName = classFullName;
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The entity unique name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            string classFullName,
            params IDataItem[] items)
            : this(name, null, classFullName, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            string name,
            params IDataItem[] items)
            : this(name, null, null, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            params IDataItem[] items)
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
            return new ObjectElementSpec();
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Adds a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public override bool AddItem(
            object item,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            bool boolean = base.AddItem(item, appScope, scriptVariableSet, log);
            if (this[0] is DataItem)
            {
                Assembly assembly = this[0].GetType().Assembly;
                this.ClassFullName = this[0].GetType().FullName.ToString()
                    + (assembly == null ? "" : "," + assembly.GetName().Name);
            }
            return boolean;
        }

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
            object object1 = null;

            if (this.Specification==null ||
                (this.Specification is ObjectElementSpec) && (this.Specification as ObjectElementSpec).ClassFilter.IsValueAllowed(this.ClassFullName))

                if (appScope != null && appScope.AppExtension!=null)
                    log.Append(appScope.AppExtension.CreateInstance(AssemblyHelper.GetClassReference(this.ClassFullName), out object1));

            return object1;
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
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
            {
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            }
            else if (indexItem is string)
            {
                return this.GetItems(appScope, scriptVariableSet, log)
                   .Any(p => p is NamedDataItem && string.Equals((p as NamedDataItem)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

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
            return string.Join("|", this.Items.Select(p => (p as NamedDataItem).Key() ?? "").ToArray());
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            object object1,
            ILog log = null)
        {
            string stringValue = "";

            if (object1 is DataItem)
            {
                DataItem item = object1 as DataItem;
                if (item != null)
                    stringValue = item.ToXml();
                else if (log != null)
                    log.AddError(title: "object expected", description: "The specified object is not an entity.");
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
            object object1 = null;

            if (stringValue != null)
                if ((this.Specification == null || this.Specification is ObjectElementSpec)
                    && (appScope != null && appScope.AppExtension!= null))
                    appScope.AppExtension.LoadDataItemInstance(AssemblyHelper.GetClassReference(this.ClassFullName), stringValue, out object1);

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
            ObjectElement aObjectElement = this.MemberwiseClone() as ObjectElement;
            //if (this.DataSchemreference != null)
            //    aObjectElement.DataSchemreference = this.DataSchemreference.Clone() as DataHandler;

            return aObjectElement;
        }

        #endregion
    }

}
