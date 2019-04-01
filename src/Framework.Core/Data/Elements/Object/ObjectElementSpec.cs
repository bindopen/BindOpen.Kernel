using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements._Object
{
    /// <summary>
    /// This class represents an object element specification.
    /// </summary>
    [Serializable()]
    [XmlType("ObjectElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ObjectElementSpec : DataElementSpec
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Entity ----------------------------------

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        [XmlElement("class.filter")]
        public IDataValueFilter ClassFilter { get; set; } = null;

        /// <summary>
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ClassFilterSpecified => ClassFilter != null
                    && (ClassFilter.AddedValues == null || ClassFilter.AddedValues.Count > 0) &&
                    (ClassFilter.RemovedValues == null || ClassFilter.RemovedValues.Count > 0);

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new object element specification.
        /// </summary>
        public ObjectElementSpec() : base()
        {
        }

        /// <summary>
        /// Initializes a new object element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ObjectElementSpec(
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
        /// Creates a new data element respecting this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <returns>Returns a new data element respecting this instance.</returns>
        public override DataElement NewElement(IAppScope appScope = null, IDataElementSet detail = null)
        {
            return new ObjectElement(Name);
        }

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


        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override ILog CheckItem(
            object item,
            IDataElement dataElement = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override ILog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            // we check that the entity unique name is available
            // we check that the format unique name is available
            // we check that the schema unique name is available

            return new Log();
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
            ObjectElementSpec specification = base.Clone() as ObjectElementSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as DataValueFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion
    }
}
