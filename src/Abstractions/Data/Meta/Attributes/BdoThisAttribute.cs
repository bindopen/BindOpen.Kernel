using System;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class BdoThisAttribute : BdoParameterAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoThisAttribute class.
        /// </summary>
        public BdoThisAttribute() : base()
        {
        }

        public BdoThisAttribute(
            string name,
            DataValueTypes valueType = DataValueTypes.Any,
            RequirementLevels requirement = RequirementLevels.Any)
            : base(name, valueType, requirement)
        {
        }

        public BdoThisAttribute(
            RequirementLevels dataRequirement,
            string dataRequirementExp)
            : base(dataRequirement, dataRequirementExp)
        {
        }

        #endregion
    }
}
