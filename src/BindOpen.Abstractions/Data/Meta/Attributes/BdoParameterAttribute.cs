using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BdoParameterAttribute : Attribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The title of this instance.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RequirementLevels DataRequirement { get; set; } = RequirementLevels.Any;

        /// <summary>
        /// 
        /// </summary>
        public string DataRequirementExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RequirementLevels Requirement { get; set; } = RequirementLevels.Any;

        /// <summary>
        /// 
        /// </summary>
        public string RequirementExp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoParameterAttribute class.
        /// </summary>
        public BdoParameterAttribute() : base()
        {
        }

        public BdoParameterAttribute(
            string name,
            DataValueTypes valueType = DataValueTypes.Any,
            RequirementLevels requirement = RequirementLevels.Any)
        {
            Name = name;
            ValueType = valueType;
            Requirement = requirement;
        }

        public BdoParameterAttribute(
            RequirementLevels dataRequirement,
            string dataRequirementExp)
        {
            DataRequirement = dataRequirement;
            DataRequirementExp = dataRequirementExp;
        }

        #endregion
    }
}
