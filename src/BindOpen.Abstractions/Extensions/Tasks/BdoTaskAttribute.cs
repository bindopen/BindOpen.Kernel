using System;

namespace BindOpen.Extensions.Tasks
{
    /// <summary>
    /// This class represents an attribute of tasks.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BdoTaskAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskAttribute class.
        /// </summary>
        public BdoTaskAttribute() : base()
        {
        }

        #endregion
    }
}
