using BindOpen.System.Scoping;
using System;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BdoInputAttribute : BdoPropertyAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoInputAttribute class.
        /// </summary>
        public BdoInputAttribute() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoInputAttribute class.
        /// </summary>
        public BdoInputAttribute(string name) : base(name)
        {
            GroupId = IBdoTaskExtensions.__Token_Input;
        }

        #endregion
    }
}
