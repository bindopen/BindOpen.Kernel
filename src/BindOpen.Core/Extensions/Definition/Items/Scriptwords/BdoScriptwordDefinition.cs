using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a carrier definition.
    /// </summary>
    public class BdoScriptwordDefinition : DataItem, IBdoScriptwordDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The library of this instance.
        /// </summary>
        public IBdoExtensionDefinition ExtensionDefinition { get; }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBdoScriptwordDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => ExtensionDefinition?.Dto?.Name + "$" + Dto?.Name; }

        /// <summary>
        /// The runtime function of this instance.
        /// </summary>
        public BdoScriptwordFunction RuntimeFunction { get; set; }

        /// <summary>
        /// The parent of this instance.
        /// </summary>
        public IBdoScriptwordDefinition Parent { get; set; }

        /// <summary>
        /// The definitions of the sub words of this instance.
        /// </summary>
        public Dictionary<string, IBdoScriptwordDefinition> Children { get; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordDefinition class.
        /// </summary>
        public BdoScriptwordDefinition()
        {
            Children = new Dictionary<string, IBdoScriptwordDefinition>();
        }

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordDefinition class.
        /// </summary>
        /// <param name="extension">The extension to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        /// <param name="parent">The parent to consider.</param>
        public BdoScriptwordDefinition(
            IBdoExtensionDefinition extension,
            IBdoScriptwordDefinitionDto dto,
            IBdoScriptwordDefinition parent = null) : this()
        {
            ExtensionDefinition = extension;
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
