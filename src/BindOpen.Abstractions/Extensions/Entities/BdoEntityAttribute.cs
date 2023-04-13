using System;

namespace BindOpen.Extensions.Entities
{
    /// <summary>
    /// This class represents a entity attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BdoEntityAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityAttribute class.
        /// </summary>
        public BdoEntityAttribute() : base()
        {
        }

        public BdoEntityAttribute(string name) : base(name)
        {
        }

        #endregion
    }
}
