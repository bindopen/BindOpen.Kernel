using BindOpen.Framework.Core.Data.Items.Attributes;
using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a script word definition attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoScriptwordDefinitionAttribute : DescribedDataItemAttribute
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinitionAttribute class.
        /// </summary>
        public BdoScriptwordDefinitionAttribute() : base()
        {
        }

        #endregion
    }
}
