using BindOpen.Data.Common;
using BindOpen.Data.Specification;
using System;

namespace BindOpen.Extensions.Carriers
{
    /// <summary>
    /// This class represents a field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoDbFieldAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueType ValueType { get; set; }

        /// <summary>
        /// The constraint statement of this instance.
        /// </summary>
        public DataConstraintStatement ConstraintStatement { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbFieldAttribute class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="constraintStatement">The constraint statement to consider.</param>
        public BdoDbFieldAttribute(DataValueType valueType,
            DataConstraintStatement constraintStatement = null) : base()
        {
            ValueType = valueType;
            ConstraintStatement = constraintStatement;
        }

        /// <summary>
        /// Instantiates a new instance of the DbFieldAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="constraintStatement">The constraint statement to consider.</param>
        public BdoDbFieldAttribute(string name,
            DataValueType valueType = DataValueType.Any,
            DataConstraintStatement constraintStatement = null) : base()
        {
            Name = name;
            ValueType = valueType;
            ConstraintStatement = constraintStatement;
        }

        #endregion
    }
}
