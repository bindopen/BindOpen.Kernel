using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    /// <summary>
    /// This class represents a data source element.
    /// </summary>
    /// <remarks>A data source element can only have one item maximum.</remarks>
    [Serializable()]
    [XmlType("SourceElement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class SourceElement : DataElement, ISourceElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [XmlElement("specification")]
        public new SourceElementSpec Specification
        {
            get { return base.Specification as SourceElementSpec; }
            set { base.Specification = value; }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element.
        /// </summary>
        public SourceElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="dataSource">The data source to consider.</param>
        public SourceElement(
            String name = null,
            IDataSource dataSource = null)
            : base(name, "dataSourceElement_")
        {
            this.ValueType = DataValueType.DataSource;
            //if (this.Specification = new SourceElementSpec();
            //this.Specification.MaximumItemNumber = 1;

            this.AddItem(dataSource);
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
            Object indexItem = null,
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
                   .Any(p => p is IDataSource && string.Equals((p as IDataSource)?.Key() ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
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
            return string.Join("|", this.Items.Select(p => (p as EntityConfiguration)?.Key() ?? "").ToArray());
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
            String stringValue = "";

            if (object1 is EntityConfiguration)
            {
                EntityConfiguration dataEntityItem = object1 as EntityConfiguration;
                if (dataEntityItem != null)
                    stringValue = dataEntityItem.ToXml();
                else if (log != null)
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
                if (this.Specification != null && appScope != null && appScope.AppExtension != null)
                    object1 = appScope.AppExtension.CreateConfiguration<ConnectorDefinition>(null, stringValue, log);

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
            SourceElement dataSourceElement = this.MemberwiseClone() as SourceElement;
            return dataSourceElement;
        }

        #endregion
    }
}
