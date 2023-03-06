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
        /// Indicates whether this instance can be repeated in set.
        /// </summary>
        public bool IsRepeated { get; set; }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public RequirementLevels RequirementLevel { get; set; }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public RequirementLevels ItemRequirementLevel { get; set; }

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

        /// <summary>
        /// Instantiates a new instance of the BdoParameterAttribute class.
        /// </summary>
        public BdoParameterAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
