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
