using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This class represents a data source element.
    /// </summary>
    /// <remarks>A data source element can only have one item maximum.</remarks>
    [XmlType("SourceElement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class SourceElement : DataElement, ISourceElement
    {
        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        [XmlIgnore()]
        public new IBdoConnectorConfiguration this[int index] => base[index] as BdoConnectorConfiguration;

        /// <summary>
        /// Returns the element with the specified unique name.
        /// </summary>
        [XmlIgnore()]
        public new IBdoConnectorConfiguration this[string name] => base[name] as BdoConnectorConfiguration;

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; } = "";

        // --------------------------------------------------

        /// <summary>
        /// Returns the first item.
        /// </summary>
        [XmlIgnore()]
        public new IBdoConnectorConfiguration First => this[0];

        /// <summary>
        /// Connectors of this instance.
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoConnectorConfiguration> Connectors
        {
            get;
            set;
        }

        /// <summary>
        /// Specification of the Connectors property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConnectorsSpecified => Items.Count > 0;

        // --------------------------------------------------

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
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        public SourceElement() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public SourceElement(
            string name = null,
            string id = null)
            : base(name, "source_", id)
        {
            ValueType = DataValueType.Datasource;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// Creates a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public override DataElementSpec NewSpecification()
        {
            return Specification = new SourceElementSpec();
        }

        // Items ----------------------------

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
            return string.Join("|", this.Items.Select(p => (p as BdoEntityConfiguration)?.Key() ?? "").ToArray());
        }

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
            SourceElement dataSourceElement = base.Clone() as SourceElement;
            return dataSourceElement;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            Connectors = Items?.Select(p =>
            {
                BdoConnectorConfiguration configuration = p as BdoConnectorConfiguration;
                configuration?.UpdateStorageInfo(log);
                return configuration;
            }).ToList();
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            SetItems(Connectors?.Select(p =>
            {
                p.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                return p;
            }).ToArray());
        }

        #endregion
    }
}
