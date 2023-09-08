using System;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BdoPropertyAttribute : Attribute
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
        public string GroupId { get; set; }

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
        public RequirementLevels ItemRequirement { get; set; } = RequirementLevels.Any;

        /// <summary>
        /// 
        /// </summary>
        public string ItemRequirementExp { get; set; }

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

        /// <summary>
        /// The alias of the entry.
        /// </summary>
        public string[] Aliases { get; set; }

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

        /// <summary>
        /// The reference of this instance.
        /// </summary>
        public string Reference { get; set; }

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
            params string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public BdoPropertyAttribute(
            string name,
            DataValueTypes valueType,
            RequirementLevels requirement = RequirementLevels.Any)
        {
            Name = name;
            ValueType = valueType;
            Requirement = requirement;
        }

        public BdoPropertyAttribute(
            RequirementLevels dataRequirement,
            string dataRequirementExp)
        {
            ItemRequirement = dataRequirement;
            ItemRequirementExp = dataRequirementExp;
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
