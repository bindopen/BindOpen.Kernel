using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This class represents a library definition.
    /// </summary>
    public class BdoExtensionDefinition : DataItem, IBdoExtensionDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IBdoExtensionDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        public string UniqueId { get => Dto?.Name; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionDefinition class.
        /// </summary>
        public BdoExtensionDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionDefinition class.
        /// </summary>
        /// <param name="dto">The DTO item to consider.</param>
        public BdoExtensionDefinition(IBdoExtensionDefinitionDto dto)
        {
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
