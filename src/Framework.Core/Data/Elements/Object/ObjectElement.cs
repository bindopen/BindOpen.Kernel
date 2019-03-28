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
    /// This class represents a entity element that is an element whose items are entities.
    /// </summary>
    [Serializable()]
    [XmlType("ObjectElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "object", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ObjectElement : DataElement
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private String _ClassFullName = "";
        //private String _FormatUniqueName = "";
        //private DataHandler _DataSchemreference = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Object -----------------------------

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        [XmlAttribute("class")]
        public String ClassFullName
        {
            get { return this._ClassFullName; }
            set { this._ClassFullName = value; }
        }

        //// Format -----------------------------

        ///// <summary>
        ///// The format unique name of this instance.
        ///// </summary>
        //[XmlElement("formatUniqueName")]
        //public String FormatUniqueName
        //{
        //    get { return this._FormatUniqueName; }
        //    set { this._FormatUniqueName = value; }
        //}

        //// Data schema ------------------------

        ///// <summary>
        ///// The data schema reference of this instance.
        ///// </summary>
        //[XmlElement("dataSchema.reference")]
        //public DataHandler DataSchemreference
        //{
        //    get { return this._DataSchemreference; }
        //    set { this._DataSchemreference = value; }
        //}

        // Specifcation -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new ObjectElementSpecification Specification
        {
            get { return base.Specification as ObjectElementSpecification; }
            set { base.Specification = value; }
        }

        // Items -----------------------

        ///// <summary>
        ///// The item object of this instance.
        ///// </summary>
        //[XmlAnyElement("object.item")]
        //public XElement ItemObject
        //{
        //    get {
        //        return XElement.Parse("<item>" + base.StringItem + "</item>");
        //    }
        //    set { base.StringItem = (value == null ? null : value.ToString()); }
        //}

        ///// <summary>
        ///// Specification of the ItemObject property of this instance.
        ///// </summary>
        //[XmlIgnore()]
        //public Boolean ItemObjectSpecified
        //{
        //    get
        //    {
        //        return base.StringItems !=null && base.StringItems.Count == 1;
        //    }
        //}

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
            String name,
            String id,
            String classFullName,
            ObjectElementSpecification aSpecification,
            params DataItem[] items)
            : base(name, "ObjectElement_", id)
        {
            this.ValueType = DataValueType.Object;
            this.Specification = aSpecification;
            
            this.SetItem(items);
            if (!String.IsNullOrEmpty(classFullName))
                this._ClassFullName = classFullName;
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="classFullName">The entity unique name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            String name,
            String classFullName,
            params DataItem[] items)
            : this(name, null, classFullName, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
            String name,
            params DataItem[] items)
            : this(name, null, null, null, items)
        {
        }

        /// <summary>
        /// Initializes a new data entity element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public ObjectElement(
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
        public override DataElementSpecification CreateSpecification()
        {
            return new ObjectElementSpecification();
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
        public override Boolean AddItem(
            Object item,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            Boolean boolean = base.AddItem(item, appScope, scriptVariableSet, log);
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
            Object item,
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
        public override Object NewItem(IAppScope appScope = null, Log log = null)
        {
            Object object1 = null;

            if (this.Specification==null ||
                (this.Specification is ObjectElementSpecification) && (this.Specification as ObjectElementSpecification).ClassFilter.IsValueAllowed(this._ClassFullName))

                if (appScope != null && appScope.AppExtension!=null)
                    log.Append(appScope.AppExtension.CreateInstance(AssemblyHelper.GetClassReference(this._ClassFullName), out object1));

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
            return String.Join("|", this.Items.Select(p => (p as NamedDataItem).Key() ?? "").ToArray());
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

            if (object1 is DataItem)
            {
                DataItem item = object1 as DataItem;
                if (item != null)
                    stringValue = item.ToXml();
                else if (log != null)
                    log.AddError(title: "Object expected", description: "The specified object is not an entity.");
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
                if ((this.Specification == null || this.Specification is ObjectElementSpecification)
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
        public override Object Clone()
        {
            ObjectElement aObjectElement = this.MemberwiseClone() as ObjectElement;
            //if (this.DataSchemreference != null)
            //    aObjectElement.DataSchemreference = this.DataSchemreference.Clone() as DataHandler;

            return aObjectElement;
        }

        #endregion

    }

}
