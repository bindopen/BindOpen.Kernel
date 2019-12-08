using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Attributes;
using BindOpen.Framework.Core.System.Scripting;
using System;

namespace BindOpen.Framework.Core.Extensions.Attributes
{
    /// <summary>
    /// This class represents a script word attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BdoScriptwordAttribute : DescribedDataItemAttribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// The sets of parameters of this instance.
        /// </summary>
        public DataElementSpecSet ParameterSet { get; set; } = new DataElementSpecSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordAttribute class.
        /// </summary>
        public BdoScriptwordAttribute() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptwordAttribute class.
        /// </summary>
        public BdoScriptwordAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
