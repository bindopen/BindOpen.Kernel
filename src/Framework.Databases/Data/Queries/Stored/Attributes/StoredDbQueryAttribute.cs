using BindOpen.Framework.Data.Items;
using System;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// This class represents a database precompiled query attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class StoredDbQueryAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the PrecompiledDbQueryAttribute class.
        /// </summary>
        public StoredDbQueryAttribute() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the PrecompiledDbQueryAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public StoredDbQueryAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
