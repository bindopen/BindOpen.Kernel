using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition
{
    /// <summary>
    /// This class represents a carrier definition.
    /// </summary>
    public class ScriptwordDefinition : DataItem, IScriptwordDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IScriptwordDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => Library?.Name + "$" + Dto?.Name; set { } }

        /// <summary>
        /// The runtime function of this instance.
        /// </summary>
        public ScriptwordFunction RuntimeFunction { get; set; }

        /// <summary>
        /// The library of this instance.
        /// </summary>
        public ILibrary Library { get; }

        /// <summary>
        /// The parent of this instance.
        /// </summary>
        public IScriptwordDefinition Parent { get; set; }

        /// <summary>
        /// The definitions of the sub words of this instance.
        /// </summary>
        public List<IScriptwordDefinition> Children { get; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        public ScriptwordDefinition()
        {
            Children = new List<IScriptwordDefinition>();
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="parent">The parent to consider.</param>
        public ScriptwordDefinition(
            ILibrary library,
            IScriptwordDefinitionDto dto,
            IScriptwordDefinition parent = null) : this()
        {
            Library = library;
            Dto = dto;
            Parent = parent;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return UniqueId;
        }

        #endregion
    }
}
