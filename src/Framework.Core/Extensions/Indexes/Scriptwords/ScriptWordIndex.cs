using System;
using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    /// <summary>
    /// This class represents a script word index.
    /// </summary>
    public class ScriptwordIndex : TAppExtensionItemIndex<IScriptwordDefinition>, IScriptwordIndex
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordIndex class.
        /// </summary>
        public ScriptwordIndex() : base()
        {
            Id = IdentifiedDataItem.NewGuid();
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptwordIndex class.
        /// </summary>
        /// <param name="definitions">The definitions of this instance.</param>
        public ScriptwordIndex(IScriptwordDefinition[] definitions) : base(definitions)
        {
        }

        #endregion

        // Tree ---------------------------

        /// <summary>
        /// Sets the specified definitions of this instance.
        /// </summary>
        /// <param name="definitions">The definitions to consider.</param>
        public override void SetDefinitions(List<IScriptwordDefinition> definitions)
        {
            Definitions = definitions;
        }
    }
}
