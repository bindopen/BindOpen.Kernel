using BindOpen.Data.Items;
using System;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    public class BdoRoutineDefinition : DataItem, IBdoRoutineDefinition
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
        public IBdoRoutineDefinitionDto Dto { get; }

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
        /// Instantiates a new instance of the RoutineDefinition class.
        /// </summary>
        public BdoRoutineDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineDefinition class.
        /// </summary>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public BdoRoutineDefinition(IBdoExtensionDefinition extensionDefinition, IBdoRoutineDefinitionDto dto)
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
