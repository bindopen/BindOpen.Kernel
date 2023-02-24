using System;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data element attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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
            GroupId = "input";
        }

        #endregion
    }
}
