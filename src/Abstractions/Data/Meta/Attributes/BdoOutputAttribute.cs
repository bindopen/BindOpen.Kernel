using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BdoOutputAttribute : BdoPropertyAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoOutputAttribute class.
        /// </summary>
        public BdoOutputAttribute() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoOutputAttribute class.
        /// </summary>
        public BdoOutputAttribute(string name) : base(name)
        {
            GroupId = IBdoTaskExtensions.__Token_Output;
        }

        #endregion
    }
}
