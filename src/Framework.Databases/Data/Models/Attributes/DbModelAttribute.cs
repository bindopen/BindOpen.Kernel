using System;

namespace BindOpen.Framework.Data.Models
{
    /// <summary>
    /// This class represents a database model attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbModelAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbModelAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public DbModelAttribute(string name = null) : base()
        {
        }

        #endregion
    }
}
