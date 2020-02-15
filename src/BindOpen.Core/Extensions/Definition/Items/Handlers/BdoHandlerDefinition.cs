using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.Extensions.Runtime;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    public class BdoHandlerDefinition : DataItem, IBdoHandlerDefinition
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
        public IBdoHandlerDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => ExtensionDefinition?.Dto?.Name + "$" + Dto?.Name; set { } }

        /// <summary>
        /// Runtime GET function of this instance.
        /// </summary>
        public BdoHandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// Runtime POST function of this instance.
        /// </summary>
        public BdoHandlerPostFunction RuntimeFunctionPost { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        public BdoHandlerDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public BdoHandlerDefinition(IBdoExtensionDefinition extensionDefinition, IBdoHandlerDefinitionDto dto)
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
