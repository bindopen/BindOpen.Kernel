using System;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a database query attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbQueryDepotAttribute : Attribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryDepotAttribute class.
        /// </summary>
        public DbQueryDepotAttribute() : base()
        {
        }

        #endregion
    }
}
