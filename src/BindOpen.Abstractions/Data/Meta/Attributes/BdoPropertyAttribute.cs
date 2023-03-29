using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BdoPropertyAttribute : BdoParameterAttribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The alias of the entry.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The minimum item number of this instance.
        /// </summary>
        public uint? MinDataItemNumber { get; set; }

        /// <summary>
        /// The maximum item number of this instance.
        /// </summary>
        public uint? MaxDataItemNumber { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public SpecificationLevels[] SpecLevels { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoPropertyAttribute class.
        /// </summary>
        public BdoPropertyAttribute() : base()
        {
        }

        public BdoPropertyAttribute(
            string name,
            DataValueTypes valueType = DataValueTypes.Any,
            RequirementLevels requirement = RequirementLevels.Any)
            : base(name, valueType, requirement)
        {
        }

        public BdoPropertyAttribute(
            RequirementLevels dataRequirement,
            string dataRequirementExp)
            : base(dataRequirement, dataRequirementExp)
        {
        }

        public BdoPropertyAttribute(uint min, uint max)
        {
            MinDataItemNumber = min;
            MaxDataItemNumber = max;
        }

        public BdoPropertyAttribute(
            params SpecificationLevels[] levels)
        {
            SpecLevels = levels;
        }

        #endregion
    }
}
