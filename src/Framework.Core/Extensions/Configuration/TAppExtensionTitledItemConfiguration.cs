using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    /// <summary>
    /// This class represents an application extension titled item configuration.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public abstract class TAppExtensionTitledItemConfiguration<T> : TAppExtensionItemConfiguration<T>, ITAppExtensionTitledItemConfiguration<T>
        where T : IAppExtensionItemDefinition
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
        public IDictionaryDataItem Title { get; set; } = null;

        /// <summary>
        /// Specification of the Title property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool TitleSpecified => this.Title != null && (this.Title.AvailableKeysSpecified || this.Title.ValuesSpecified || this.Title.SingleValueSpecified);

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        public IDictionaryDataItem Description { get; set; } = null;

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DescriptionSpecified => this.Description != null && (this.Description.AvailableKeysSpecified || this.Description.ValuesSpecified || this.Description.SingleValueSpecified);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionTitledItemConfiguration class.
        /// </summary>
        protected TAppExtensionTitledItemConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionTitledItemConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        protected TAppExtensionTitledItemConfiguration(
            string name,
            string namePreffix = null,
            T definition = default)
            : base(name, definition, namePreffix)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionTitledItemConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        protected TAppExtensionTitledItemConfiguration(
            string name,
            string namePreffix,
            string definitionUniqueId)
            : base(name, namePreffix, definitionUniqueId)
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
            this.AddTitleText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddTitleText(string key, string text)
        {
            if (this.Title == null) this.Title = new DictionaryDataItem();
            this.Title.AddValue(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(string text)
        {
            this.SetTitleText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(string key = "*", string text = "*")
        {
            if (this.Title == null) this.Title = new DictionaryDataItem();
            this.Title.SetValue(key, text);
        }

        // Description -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(string text)
        {
            this.AddDescriptionText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(string key, string text)
        {
            if (this.Description == null) this.Description = new DictionaryDataItem();
            this.Description.AddValue(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(string text)
        {
            this.SetDescriptionText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(string key = "*", string text = "*")
        {
            (this.Description ?? (this.Description = new DictionaryDataItem())).SetValue(key, text);
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
            if (this.Title == null) return "";
            string label = this.Title.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = this.Title.GetContent(defaultVariantName);
            if (string.IsNullOrEmpty(label))
                label = this.Name;
            return label ?? "";
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public string GetDescriptionText(string variantName = "*", string defaultVariantName = "*")
        {
            if (this.Description == null) return "";
            string label = this.Description.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = this.Description.GetContent(defaultVariantName);
            return label ?? "";
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
            TAppExtensionTitledItemConfiguration<T> appExtensionItem = base.Clone() as TAppExtensionTitledItemConfiguration<T>;
            if (this.Title != null)
                appExtensionItem.Title = this.Title.Clone() as DictionaryDataItem;
            if (this.Description != null)
                appExtensionItem.Description = this.Description.Clone() as DictionaryDataItem;

            return appExtensionItem;
        }

        #endregion
    }

}
