using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Source
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [Serializable()]
    [XmlType("SourceElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class SourceElementSpec : DataElementSpec, ISourceElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        public DataSourceKind DataSourceKind { get; set; } = DataSourceKind.Any;

        /// <summary>
        /// The connector filter of this instance.
        /// </summary>
        [XmlElement("connector.filter")]
        public IDataValueFilter ConnectorFilter { get; set; } = new DataValueFilter();

        /// <summary>
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConnectorFilterSpecified
        {
            get
            {
                return ConnectorFilter != null
                    && (ConnectorFilter.AddedValues == null || ConnectorFilter.AddedValues.Count > 0) &&
                    (ConnectorFilter.RemovedValues == null || ConnectorFilter.RemovedValues.Count > 0);
            }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public SourceElementSpec(): this(AccessibilityLevel.Public)
        {
        }

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public SourceElementSpec(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            SpecificationLevel[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override bool IsCompatibleWith(IDataItem item)
        {
            bool isCompatible = base.IsCompatibleWith(item);

            if (isCompatible)
            {
            }

            return isCompatible;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair
    

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
            SourceElementSpec specification = base.Clone() as SourceElementSpec;
            if (ConnectorFilter != null)
                specification.ConnectorFilter = ConnectorFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }

}
