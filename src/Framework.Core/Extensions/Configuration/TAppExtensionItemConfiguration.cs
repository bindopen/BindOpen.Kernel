using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public abstract class TAppExtensionItemConfiguration<T> : NamedDataItem, ITAppExtensionItemConfiguration<T>
        where T : IAppExtensionItemDefinition
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        protected T _definition = default;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// Definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// Specification of the DefinitionUniqueName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionUniqueIdSpecified => !string.IsNullOrEmpty(DefinitionUniqueId);

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [XmlIgnore()]
        public T Definition
        {
            get { return _definition; }
        }

        /// <summary>
        /// The group of this instance.
        /// </summary>
        [XmlElement("group")]
        public string Group { get; set; } = "";

        /// <summary>
        /// Specification of the Group property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool GroupSpecified => !string.IsNullOrEmpty(Group);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemConfiguration class.
        /// </summary>
        protected TAppExtensionItemConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        protected TAppExtensionItemConfiguration(
            string name,
            T definition = default,
            string namePreffix = null)
            : this(name, definition?.Key(), namePreffix)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        protected TAppExtensionItemConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix)
            : base(name, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the extension item kind of this instance.
        /// </summary>
        public AppExtensionItemKind GetExtensionItemKind()
        {
            return typeof(T).GetExtensionItemKind();
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified definition.
        /// </summary>
        /// <param name="definition"></param>
        public void SetDefinition(T definition)
        {
            this._definition = definition;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            TAppExtensionItemConfiguration<T> appExtensionItem = base.Clone() as TAppExtensionItemConfiguration<T>;
            appExtensionItem._definition=  Definition;

            return appExtensionItem;
        }

        #endregion
    }
}
