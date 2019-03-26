using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [Serializable()]
    [XmlType("ScalarElementSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScalarElementSpecification : DataElementSpecification
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        [XmlAttribute("valueType")]
        public new DataValueType ValueType
        {
            get { return base.ValueType; }
            set { base.ValueType = value; }
        }

        /// <summary>
        /// Specification of the ValueType property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ValueTypeSpecified
        {
            get
            {
                return base.ValueType != DataValueType.Any;
            }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpecification class.
        /// </summary>
        public ScalarElementSpecification() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpecification class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpecification(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpecification class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="elementRequirementLevel">The element requirement level to consider.</param>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpecification(
            String name,
            DataValueType dataValueType = DataValueType.Text,
            RequirementLevel elementRequirementLevel = RequirementLevel.Required,
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
            this.Name = name;
            this.ValueType = dataValueType;
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpecification class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="elementRequirementLevel">The element requirement level to consider.</param>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpecification(
            String name,
            Type type,
            RequirementLevel elementRequirementLevel = RequirementLevel.Required,
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
            if (type != null)
            {
                DataElement dataElement = DataElement.Create(type.GetValueType(), name);

                if ((dataElement != null) && (dataElement.Specification != null))
                {
                    dataElement.Specification.DesignStatement.ControlType = type.GetDefaultControlType();

                    if (type.IsArray)
                        dataElement.Specification.MaximumItemNumber = -1;
                    else if (type.IsEnum)
                        dataElement.Specification.ConstraintStatement.AddConstraint(
                            null, "standard$" + BasicRoutineKind.ItemMustBeInList, new DataElementSet(
                                DataElement.Create(type.GetFields().Select(p => p.Name).ToList().Cast<Object>(), DataValueType.Text)));
                }
            }
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
        public override DataElement NewElement(IAppScope appScope = null, DataElementSet detail = null)
        {
            return ScalarElement.Create(this.DefaultItems, this.ValueType, this.Name);
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override Boolean IsCompatibleWith(DataItem item)
        {
            Boolean isCompatible = base.IsCompatibleWith(item);

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
        public override Log CheckItem(
            Object item,
            DataElement dataElement = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
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
        public override Log CheckElement(
            DataElement dataElement,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
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
        public override Object Clone()
        {
            ScalarElementSpecification aScalarElementSpecification = base.Clone() as ScalarElementSpecification;
            return aScalarElementSpecification;
        }

        #endregion
    }

}
