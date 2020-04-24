using BindOpen.Application.Scopes;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents described data item.
    /// </summary>
    [XmlType("DescribedDataItem", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot("describedDataItem", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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
        public DescribedDataItem(string name,
            string namePreffix = "",
            string id = null)
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
            string name,
            string title,
            string description,
            string namePreffix = "",
            string id = null)
            : base(name, namePreffix, id)
        {
            if (title != null)
            {
                Title = ItemFactory.CreateDictionary(title);
            }

            if (description != null)
            {
                Description = ItemFactory.CreateDictionary(description);
            }
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public IDescribedDataItem WithDescription(string text)
        {
            this.WithDescription("*", text);

            return this;
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public IDescribedDataItem WithDescription(string key = "*", string text = "*")
        {
            (this.Description ?? (this.Description = new DictionaryDataItem())).Set(key, text);

            return this;
        }

        /// <summary>
        /// Updates this instance with the base object.
        /// </summary>
        /// <param name="updateBaseObject">The update base object to consider.</param>
        public void Update(IDescribedDataItem updateBaseObject)
        {
            if (updateBaseObject != null)
            {
                base.Update(updateBaseObject);

                if (updateBaseObject.Description != null)
                    this.Description = ItemFactory.CreateDictionary(updateBaseObject.Description);
            }
        }

        // Description -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public IDescribedDataItem AddDescription(string text)
        {
            this.AddDescription("*", text);

            return this;
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public IDescribedDataItem AddDescription(string key, string text)
        {
            (this.Description ?? (this.Description = new DictionaryDataItem())).Add(key, text);

            return this;
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
        public virtual string GetDescription(string variantName = "*", string defaultVariantName = "*")
        {
            if (this.Description == null) return "";
            string label = this.Description.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = this.Description.GetContent(defaultVariantName);
            return label ?? "";
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var item = base.Clone(areas) as DescribedDataItem;
            if (this.Description != null)
                item.Description = this.Description.Clone<DictionaryDataItem>();

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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);
            this.Description?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            Description?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
