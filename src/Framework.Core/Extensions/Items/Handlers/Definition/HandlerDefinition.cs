using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Handlers.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items.Handlers.Definition
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    public class HandlerDefinition : DataItem, IHandlerDefinition
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
        public IHandlerDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => Library?.Name + "$" + Dto?.Name; set { } }

        /// <summary>
        /// Runtime GET function of this instance.
        /// </summary>
        public HandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// Runtime POST function of this instance.
        /// </summary>
        public HandlerPostFunction RuntimeFunctionPost { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        public HandlerDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public HandlerDefinition(ILibrary library, IHandlerDefinitionDto dto)
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
