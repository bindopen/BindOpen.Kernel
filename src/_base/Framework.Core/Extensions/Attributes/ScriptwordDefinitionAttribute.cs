using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a script word definition attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptwordDefinitionAttribute : AppExtensionItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinitionAttribute class.
        /// </summary>
        public ScriptwordDefinitionAttribute() : base()
        {
        }

        #endregion
    }
}
