using BindOpen.Data.Items;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension item configuration.
    /// </summary>
    public abstract class TBdoExtensionItem<D, C, T> : BdoItem,
        ITBdoExtensionItem<D, C, T>
        where D : IBdoExtensionItemDefinition
        where C : ITBdoExtensionItemConfiguration<D>
        where T : class, IBdoExtensionItem
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
        // ITBdoExtensionItem<D, C, T> Implementation
        // -----------------------------------------------

        #region ITBdoExtensionItem<D, C, T>

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        public C Config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public T WithConfig(C config)
        {
            Config = config;
            return this as T;
        }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        public D Definition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public T WithDefinition(D def)
        {
            Definition = def;
            return this as T;
        }

        #endregion

        // ------------------------------------------
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T WithId(string id)
        {
            Id = id;
            return this as T;
        }

        #endregion
    }
}
