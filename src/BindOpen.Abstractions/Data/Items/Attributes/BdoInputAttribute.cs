using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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
            GroupId = "input";
        }

        #endregion
    }
}
