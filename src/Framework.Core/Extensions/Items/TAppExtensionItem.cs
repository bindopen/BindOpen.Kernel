using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    public abstract class TAppExtensionItem<T> : IdentifiedDataItem, ITAppExtensionItem<T>
        where T : IAppExtensionItemDefinition
    {
        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        protected ITAppExtensionItemConfiguration<T> _configuration;
        private T _definition;

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        public new ITAppExtensionItemConfiguration<T> Configuration => _configuration;

        IConfiguration IAppExtensionItem.Configuration => _configuration as IConfiguration;

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
        /// <param name="config">The configuration to consider.</param>
        protected TAppExtensionItem(ITAppExtensionItemConfiguration<T> config = default) : this()
        {
            _configuration = config;
        }

        #endregion

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public virtual IDataElement AsElement(string name)
        {
            return null;
        }

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
