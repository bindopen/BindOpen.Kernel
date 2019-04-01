using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Complex
{
    /// <summary>
    /// This class represents a meta data element that is a data element whose items are data elements.
    /// </summary>
    [Serializable()]
    [XmlType("MetaDataElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "meta", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(DocumentElement))]
    public abstract class MetaDataElement : DataElement, IMetaDataElement
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new meta data element.
        /// </summary>
        public MetaDataElement()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new meta data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        public MetaDataElement(string name = null, string namePreffix = null)
            : base(name, namePreffix)
        {
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
        public override object NewItem(IAppScope appScope = null, ILog log = null)
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
        public override object GetItem(
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            else if (indexItem is string)
                return this.Items.Any(p => p is DataElement && string.Equals((p as DataElement)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));

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
            return string.Join("|", this.Items.Select(p => !(p is DataElement) ? "" : (p as DataElement).Name.ToString()).ToArray());
        }

        // Conversion -----------------------------------

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

            if (object1 is DataElement)
                stringValue = (object1 as DataElement).ToString();

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
                object1 = XmlHelper.LoadFromString<DataElement>(stringValue, log);

            return object1;
        }

        #endregion

        // --------------------------------------------------
        // CHECK, UPDATE, REPAIR
        // --------------------------------------------------

        #region Check_Update_Repair


        #endregion
    }
}
