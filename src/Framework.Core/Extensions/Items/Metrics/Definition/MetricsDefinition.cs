using System;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Libraries;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics.Definition
{
    /// <summary>
    /// This class represents a connector definition.
    /// </summary>
    public class MetricsDefinition : DataItem, IMetricsDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The item of this instance.
        /// </summary>
        public IMetricsDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public string UniqueId { get => Library?.Name + "$" + Dto?.Name; set { } }

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
        /// Instantiates a new instance of the MetricsDefinition class.
        /// </summary>
        public MetricsDefinition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the MetricsDefinition class.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="dto">The DTO item to consider.</param>
        public MetricsDefinition(ILibrary library, IMetricsDefinitionDto dto)
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
