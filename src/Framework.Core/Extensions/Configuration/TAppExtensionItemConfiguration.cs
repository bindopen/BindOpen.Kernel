using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public abstract class TAppExtensionItemConfiguration<T> : NamedDataItem
        where T : AppExtensionItemDefinition
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private String _definitionUniqueId = "";

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        protected T _definition = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// Definition unique ID of this instance.
        /// </summary>
        [XmlAttribute("definition")]
        public String DefinitionUniqueId
        {
            get { return this._definitionUniqueId; }
            set
            {
                this._definitionUniqueId = value;
                // we reset the definition if needed
                this.SetDefinition();
            }
        }

        /// <summary>
        /// Specification of the DefinitionUniqueName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DefinitionUniqueIdSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.DefinitionUniqueId);
            }
        }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [XmlIgnore()]
        public T Definition
        {
            get { return this._definition; }
        }

        /// <summary>
        /// The group of this instance.
        /// </summary>
        [XmlElement("group")]
        public String Group { get; set; } = "";

        /// <summary>
        /// Specification of the Group property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean GroupSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this.Group);
            }
        }

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
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        protected TAppExtensionItemConfiguration(
            String name,
            String definitionUniqueId,
            T definition,
            String namePreffix = null)
            : base(name, namePreffix)
        {
            if (definitionUniqueId == null && definition != null)
                definitionUniqueId = definition.Key();
            this._definitionUniqueId = definitionUniqueId;
            this.SetDefinition(this._definition);
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
        /// Initializes the definition of this instance.
        /// </summary>
        public virtual void InitializeDefinition()
        {
        }

        /// <summary>
        /// Sets the definition of this instance.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="isDefinitionChecked">Indicates whether the definition must be checked.</param>
        public virtual void SetDefinition(T definition=null, Boolean isDefinitionChecked = true)
        {
            if (!isDefinitionChecked || (definition?.KeyEquals(this.DefinitionUniqueId) == true))
            {
                this._definition = definition;
                this._definitionUniqueId = definition?.Key();
            }
            else
            {
                this._definition = null;
            }
        }

        /// <summary>
        /// Sets the specififed definition.
        /// </summary>
        /// <param name="appExtension">The application extension to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        public void SetDefinition(AppExtension appExtension, String definitionName = null)
        {
            T definition = null;
            if (appExtension != null)
            {
                definitionName = (definitionName ?? this._definitionUniqueId);
                definition = appExtension.GetItemDefinitionWithUniqueId<T>(definitionName) as T;
                if (definition != null)
                {
                    this._definitionUniqueId = definition.Key();
                    this.SetDefinition(definition);
                }
            }
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
        public override Object Clone()
        {
            TAppExtensionItemConfiguration<T> appExtensionItem = base.Clone() as TAppExtensionItemConfiguration<T>;
            appExtensionItem.SetDefinition(this.Definition);

            return appExtensionItem;
        }

        #endregion
    }


}
