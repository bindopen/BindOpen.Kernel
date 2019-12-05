using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Elements.Scalar
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    [Serializable()]
    [XmlType("ScalarElementSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ScalarElementSpec : DataElementSpec, IScalarElementSpec
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
        public bool ValueTypeSpecified => base.ValueType != DataValueType.Any;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        public ScalarElementSpec() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public ScalarElementSpec(
            String name,
            DataValueType dataValueType = DataValueType.Text,
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
            this.Name = name;
            this.ValueType = dataValueType;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckItem(
            object item,
            IDataElement dataElement = null)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null)
        {
            return new BdoLog();
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
            ScalarElementSpec aScalarElementSpec = base.Clone() as ScalarElementSpec;
            return aScalarElementSpec;
        }

        #endregion
    }

}
