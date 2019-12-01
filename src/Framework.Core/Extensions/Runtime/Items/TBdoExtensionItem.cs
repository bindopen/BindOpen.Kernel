using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This class represents a BindOpen extension item configuration.
    /// </summary>
    public abstract class TBdoExtensionItem<T> : IdentifiedDataItem, ITBdoExtensionItem<T>
        where T : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        protected ITBdoExtensionItemConfiguration<T> _configuration;
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
        public ITBdoExtensionItemConfiguration<T> Configuration => _configuration;

        IBdoBaseConfiguration IBdoExtensionItem.Configuration => _configuration as IBdoBaseConfiguration;

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
        /// Instantiates a new instance of the TBdoExtensionItem class.
        /// </summary>
        protected TBdoExtensionItem() : base()
        {
            Id = IdentifiedDataItem.NewGuid();
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionItem class.
        /// </summary>
        /// <param name="config">The configuration to consider.</param>
        protected TBdoExtensionItem(ITBdoExtensionItemConfiguration<T> config = default) : this()
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
