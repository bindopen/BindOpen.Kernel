using System;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items.Entities.Definition
{
    /// <summary>
    /// This class represents a entity definition.
    /// </summary>
    public class EntityDefinition : DataItem, IEntityDefinition
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
        public IEntityDefinitionDto Dto { get; }

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
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        public EntityDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public EntityDefinition(ILibrary library, IEntityDefinitionDto dto)
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
