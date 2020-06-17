using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a BindOpen extension titled item configuration.
    /// </summary>
    public abstract class TBdoExtensionTitledItemConfiguration<T>
        : TBdoExtensionItemConfiguration<T>, ITBdoExtensionTitledItemConfiguration<T>
        where T : IBdoExtensionItemDefinition
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        // General -------------------------------

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [XmlElement("title")]
        public DictionaryDataItem Title { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionTitledItemConfiguration class.
        /// </summary>
        protected TBdoExtensionTitledItemConfiguration() : this(BdoExtensionItemKind.Any, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionTitledItemConfiguration class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        protected TBdoExtensionTitledItemConfiguration(
            BdoExtensionItemKind kind,
            string definitionUniqueId) : base(kind, definitionUniqueId)
        {
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        // Title -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddTitleText(string text)
        {
            AddTitleText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddTitleText(string key, string text)
        {
            (Title ?? (Title = new DictionaryDataItem())).Add(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(string text)
        {
            SetTitleText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(string key = "*", string text = "*")
        {
            if (Title == null) Title = new DictionaryDataItem();
            Title.Set(key, text);
        }

        // Description -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(string text)
        {
            AddDescriptionText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(string key, string text)
        {
            if (Description == null) Description = new DictionaryDataItem();
            Description.Add(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(string text)
        {
            SetDescriptionText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(string key = "*", string text = "*")
        {
            (Description ?? (Description = new DictionaryDataItem())).Set(key, text);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the title label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public string GetTitleText(string variantName = "*", string defaultVariantName = "*")
        {
            if (Title == null) return string.Empty;
            string label = Title.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = Title.GetContent(defaultVariantName);
            if (string.IsNullOrEmpty(label))
                label = Name;
            return label ?? string.Empty;
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public string GetDescriptionText(string variantName = "*", string defaultVariantName = "*")
        {
            if (Description == null) return string.Empty;
            string label = Description.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = Description.GetContent(defaultVariantName);
            return label ?? string.Empty;
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
        public override object Clone(params string[] areas)
        {
            ITBdoExtensionTitledItemConfiguration<T> dto = base.Clone(areas) as TBdoExtensionTitledItemConfiguration<T>;
            if (Title != null)
                dto.Title = Title.Clone() as DictionaryDataItem;

            return dto;
        }

        #endregion

        // ------------------------------------------
        // ITitledDataItem IMPLEMENTATION
        // ------------------------------------------

        #region ITitledDataItem Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public INamedDataItem WithName(string name)
        {
            Name = name;

            return this;
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public ITitledDataItem AddTitle(string text)
        {
            AddTitle("*", text);

            return this;
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public ITitledDataItem AddTitle(string key, string text)
        {
            (Title ?? (Title = new DictionaryDataItem())).Add(key, text);

            return this;
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public ITitledDataItem WithTitle(string text)
        {
            WithTitle("*", text);

            return this;
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public ITitledDataItem WithTitle(string key = "*", string text = "*")
        {
            (Title ?? (Title = new DictionaryDataItem())).Set(key, text);

            return this;
        }

        /// <summary>
        /// Returns the title label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public virtual string GetTitle(string variantName = "*", string defaultVariantName = "*")
        {
            if (Title == null) return string.Empty;

            string label = Title.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
            {
                label = Title.GetContent(defaultVariantName);
            }
            if (string.IsNullOrEmpty(label))
            {
                label = Name;
            }

            return label ?? string.Empty;
        }

        #endregion
    }

}
