using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension item config.
    /// </summary>
    public abstract class TBdoExtensionItem<T, D> : BdoItem,
        ITBdoExtensionItem<T, D>
        where T : class, IBdoExtensionItem
        where D : IBdoExtensionItemDefinition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionItem class.
        /// </summary>
        protected TBdoExtensionItem() : base()
        {
        }

        #endregion

        // -----------------------------------------------
        // ITBdoExtensionItem<T, D> Implementation
        // -----------------------------------------------

        #region ITBdoExtensionItem<T, D>

        string _definitionUniqueName;

        /// <summary>
        /// The config of this instance.
        /// </summary>
        public string DefinitionUniqueName
        {
            get => Definition?.UniqueName ?? _definitionUniqueName;
            set
            {
                if (!string.IsNullOrEmpty(Definition?.UniqueName))
                {
                    _definitionUniqueName = value;
                }
            }
        }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        public D Definition { get; set; }

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        public IBdoConfiguration Config { get; set; }

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion
    }
}
