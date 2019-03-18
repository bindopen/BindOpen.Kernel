using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents described data item.
    /// </summary>
    [Serializable()]
    [XmlType("DescribedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("describedDataItem", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DescribedDataItem : TitledDataItem, IDescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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
                return this.Description !=null && (this.Description.AvailableKeysSpecified || this.Description.ValuesSpecified || this.Description.SingleValueSpecified);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DescribedDataItem class.
        /// </summary>
        public DescribedDataItem() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DescribedDataItem class considering the specified preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        public DescribedDataItem(String name,
            String namePreffix = "",
            String id = null)
            : base(name, namePreffix, id)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DescribedDataItem class considering the specified preffix name.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        /// <param name="id">The ID to consider.</param>
        public DescribedDataItem(
            String name,
            String title,
            String description,
            String namePreffix = "",
            String id = null)
            : base(name, namePreffix, id)
        {
            if (title != null)
                this.Title = new DictionaryDataItem(title);
            if (description != null)
                this.Description = new DictionaryDataItem(description);
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
        public void Update(DescribedDataItem updateBaseObject)
        {
            if (updateBaseObject != null)
            {
                base.Update(updateBaseObject);

                if (updateBaseObject.Description != null)
                    this.Description = new DictionaryDataItem(updateBaseObject.Description);
            }
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
            (this.Description ?? (this.Description = new DictionaryDataItem())).AddValue(key, text);
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
            (this.Description ?? (this.Description = new DictionaryDataItem())).SetValue(key, text);
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public virtual String GetDescriptionText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this.Description == null) return "";
            String label = this.Description.GetContent(variantName);
            if (String.IsNullOrEmpty(label))
                label = this.Description.GetContent(defaultVariantName);
            return label ?? "";
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            DescribedDataItem item = base.Clone() as DescribedDataItem;
            if (this.Description != null)
                item.Description = this.Description.Clone() as DictionaryDataItem;

            return item;
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
            this.Description?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);
            this.Description?.UpdateRuntimeInfo(appScope, log);
        }

        #endregion
    }
}
