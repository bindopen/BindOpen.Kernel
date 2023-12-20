using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a task attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BdoTaskAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskAttribute class.
        /// </summary>
        public BdoTaskAttribute() : base()
        {
        }

        public BdoTaskAttribute(string name) : base(name)
        {
        }

        #endregion
    }
}
