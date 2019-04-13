using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Libraries;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Definition.Scriptwords
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
        public string UniqueId { get => Library?.Id + "$" + Dto?.Name; set { } }

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
        public IScriptwordDefinition Parent { get; }

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
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="parent">The parent to consider.</param>
        public ScriptwordDefinition(ILibrary library, IScriptwordDefinitionDto dto, IScriptwordDefinition parent)
        {
            Library = library;
            Dto = dto;
            Parent = parent;

            foreach(IScriptwordDefinitionDto childDto in dto?.Children)
            {
                Children.Add(new ScriptwordDefinition(library, childDto, this));
            }
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
