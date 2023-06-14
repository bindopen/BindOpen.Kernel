using System;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class BdoParameterAttribute : BdoPropertyAttribute
    {
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
            : base(name, valueType, requirement)
        {
        }

        public BdoParameterAttribute(
            RequirementLevels dataRequirement,
            string dataRequirementExp)
            : base(dataRequirement, dataRequirementExp)
        {
        }

        #endregion
    }
}
