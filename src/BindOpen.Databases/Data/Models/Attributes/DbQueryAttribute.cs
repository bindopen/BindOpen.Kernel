using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.Data.Models
{
    /// <summary>
    /// This class represents a database precompiled query attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DbQueryAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryAttribute class.
        /// </summary>
        public DbQueryAttribute() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public DbQueryAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
