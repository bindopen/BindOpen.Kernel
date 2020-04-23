using BindOpen.Application.Configuration;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
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
        public T Definition { get; private set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionItem class.
        /// </summary>
        protected TBdoExtensionItem() : base("")
        {
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
            Definition = definition;
        }
    }
}
