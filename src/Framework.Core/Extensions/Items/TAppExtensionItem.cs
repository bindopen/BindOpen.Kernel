using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    public abstract class TAppExtensionItem<T> : IdentifiedDataItem, ITAppExtensionItem<T>
        where T : IAppExtensionItemDefinition
    {
        private ITAppExtensionItemDto<T> _dto;
        private T _definition;

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The DTO item of this instance.
        /// </summary>
        public ITAppExtensionItemDto<T> Dto => _dto;

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        public T Definition => _definition;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItem class.
        /// </summary>
        protected TAppExtensionItem() : base()
        {
            Id = IdentifiedDataItem.NewGuid();
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItem class.
        /// </summary>
        /// <param name="dto">The DTO item to consider.</param>
        protected TAppExtensionItem(ITAppExtensionItemDto<T> dto = default) : this()
        {
            _dto = dto;
        }

        #endregion


        /// <summary>
        /// Sets the specified definition of this instance.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        public void SetDefinition(T definition)
        {
            _definition = definition;
        }
    }
}
