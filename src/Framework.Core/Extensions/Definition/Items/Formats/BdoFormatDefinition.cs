using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;
using System;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This class represents a format definition.
    /// </summary>
    public class BdoFormatDefinition : DataItem, IBdoFormatDefinition
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
        public IBdoFormatDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => ExtensionDefinition?.Dto?.Name + "$" + Dto?.Name; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        public BdoFormatDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public BdoFormatDefinition(IBdoExtensionDefinition extensionDefinition, IBdoFormatDefinitionDto dto)
        {
            ExtensionDefinition = extensionDefinition;
            Dto = dto;
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
