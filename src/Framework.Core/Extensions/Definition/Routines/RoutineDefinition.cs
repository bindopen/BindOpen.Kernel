using System;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Libraries;

namespace BindOpen.Framework.Core.Extensions.Definition.Routines
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    public class RoutineDefinition : DataItem, IRoutineDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IRoutineDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => Library?.Id + "$" + Dto?.Name; set { } }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The library of this instance.
        /// </summary>
        public ILibrary Library { get; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RoutineDefinition class.
        /// </summary>
        public RoutineDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RoutineDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public RoutineDefinition(ILibrary library, IRoutineDefinitionDto dto)
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
