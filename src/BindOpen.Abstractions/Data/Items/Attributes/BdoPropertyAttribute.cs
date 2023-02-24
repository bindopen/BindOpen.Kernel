using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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
        /// ID of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The alias of the entry.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The minimum item number of this instance.
        /// </summary>
        public int? MinimumItemNumber { get; set; }

        /// <summary>
        /// The maximum item number of this instance.
        /// </summary>
        public int? MaximumItemNumber { get; set; }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public RequirementLevels RequirementLevel { get; set; }

        /// <summary>
        /// Levels of specification of this instance.
        /// </summary>
        public SpecificationLevels SpecificationLevel { get; set; }

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

        /// <summary>
        /// Instantiates a new instance of the BdoPropertyAttribute class.
        /// </summary>
        public BdoPropertyAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
