using System;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items.Formats.Definition
{
    /// <summary>
    /// This class represents a format definition.
    /// </summary>
    public class FormatDefinition : DataItem, IFormatDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The library of this instance.
        /// </summary>
        public ILibrary Library { get; }

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IFormatDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => Library?.Name + "$" + Dto?.Name; set { } }

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
        public FormatDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public FormatDefinition(ILibrary library, IFormatDefinitionDto dto)
        {
            Library = library;
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
