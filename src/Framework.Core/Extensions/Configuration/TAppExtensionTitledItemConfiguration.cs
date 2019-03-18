using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Configuration
{

    /// <summary>
    /// This class represents an application extension titled item configuration.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public abstract class TAppExtensionTitledItemConfiguration<T> : TAppExtensionItemConfiguration<T>
        where T : AppExtensionItemDefinition
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

        /// <summary>
        /// Specification of the Title property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean TitleSpecified
        {
            get
            {
                return this.Title != null && (this.Title.AvailableKeysSpecified || this.Title.ValuesSpecified || this.Title.SingleValueSpecified);
            }
        }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        public DictionaryDataItem Description { get; set; } = null;

        /// <summary>
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DescriptionSpecified
        {
            get
            {
                return this.Description != null && (this.Description.AvailableKeysSpecified || this.Description.ValuesSpecified || this.Description.SingleValueSpecified);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionTitledItemConfiguration class.
        /// </summary>
        public TAppExtensionTitledItemConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the TAppExtensionTitledItemConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        public TAppExtensionTitledItemConfiguration(
            String name,
            String definitionName = null,
            T definition = null,
            String namePreffix = null)
            : base(name, definitionName, definition, namePreffix)
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
        public void AddTitleText(String text)
        {
            this.AddTitleText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddTitleText(String key, String text)
        {
            if (this.Title == null) this.Title = new DictionaryDataItem();
            this.Title.AddValue(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(String text)
        {
            this.SetTitleText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetTitleText(String key = "*", String text = "*")
        {
            if (this.Title == null) this.Title = new DictionaryDataItem();
            this.Title.SetValue(key, text);
        }

        // Description -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(String text)
        {
            this.AddDescriptionText("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddDescriptionText(String key, String text)
        {
            if (this.Description == null) this.Description = new DictionaryDataItem();
            this.Description.AddValue(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(String text)
        {
            this.SetDescriptionText("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetDescriptionText(String key = "*", String text = "*")
        {
            if (this.Description == null) this.Description = new DictionaryDataItem();
            this.Description.SetValue(key, text);
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
        public String GetTitleText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this.Title == null) return "";
            String label = this.Title.GetContent(variantName);
            if (String.IsNullOrEmpty(label))
                label = this.Title.GetContent(defaultVariantName);
            if (String.IsNullOrEmpty(label))
                label = this.Name;
            return (label ?? "");
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public String GetDescriptionText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this.Description == null) return "";
            String label = this.Description.GetContent(variantName);
            if (String.IsNullOrEmpty(label))
                label = this.Description.GetContent(defaultVariantName);
            return (label ?? "");
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
