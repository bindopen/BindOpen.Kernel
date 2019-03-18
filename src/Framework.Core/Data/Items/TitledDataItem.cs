using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents titled data item.
    /// </summary>
    [Serializable()]
    [XmlType("TitledDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("titledDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TitledDataItem : NamedDataItem, ITitledDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [XmlElement("title")]
        public DictionaryDataItem Title { get; set; } = null;

        /// <summary>
        /// Specification of the Title property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean TitleSpecified => this.Title != null && (this.Title.AvailableKeysSpecified || this.Title.ValuesSpecified || this.Title.SingleValueSpecified);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TitledDataItem class.
        /// </summary>
        public TitledDataItem() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TitledDataItem class considering the specified preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        public TitledDataItem(String name,
            String namePreffix = "",
            String id = null)
            : base(name, namePreffix, id)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TitledDataItem class considering the specified preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        public TitledDataItem(
            String name,
            String title,
            String namePreffix = "",
            String id = null)
            : base(name, namePreffix, id)
        {
            if (title != null)
                this.Title = new DictionaryDataItem(title);
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance with the base object.
        /// </summary>
        /// <param name="updateBaseObject">The update base object to consider.</param>
        public void Update(TitledDataItem updateBaseObject)
        {
            if (updateBaseObject != null)
            {
                if (updateBaseObject.Title != null)
                    this.Title = new DictionaryDataItem(updateBaseObject.Title);
            }
        }

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
            (this.Title ?? (this.Title = new DictionaryDataItem())).AddValue(key, text);
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
            (this.Title ?? (this.Title = new DictionaryDataItem())).SetValue(key, text);
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
        public virtual String GetTitleText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this.Title == null) return "";
            String label = this.Title.GetContent(variantName);
            if (String.IsNullOrEmpty(label))
                label = this.Title.GetContent(defaultVariantName);
            if (String.IsNullOrEmpty(label))
                label = this.Name;
            return label ?? "";
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            TitledDataItem describedDataItem = base.Clone() as TitledDataItem;
            if (this.Title != null)
                describedDataItem.Title = this.Title.Clone() as DictionaryDataItem;

            return describedDataItem;
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region Serialization_Unserialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(Log log = null)
        {
            base.UpdateStorageInfo(log);
            this.Title?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            this.Title?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
