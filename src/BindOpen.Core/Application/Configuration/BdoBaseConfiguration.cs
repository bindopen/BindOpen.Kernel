using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("Configuration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("config", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoBaseConfiguration : DataElementSet, IBdoBaseConfiguration
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [XmlElement("description")]
        public DictionaryDataItem Description { get; set; } = null;

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [XmlAttribute("name")]
        [DefaultValue("")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Current file path of this instance.
        /// </summary>
        [XmlIgnore()]
        public string CurrentFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [XmlElement("creationDate")]
        public string CreationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [XmlElement("lastModificationDate")]
        public string LastModificationDate
        {
            get;
            set;
        }
        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        public BdoBaseConfiguration() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified elements into the specified group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns this instance.</returns>
        public IBdoBaseConfiguration AddGroup(string groupId, params IDataElement[] items)
        {
            if (items != null)
            {
                foreach (DataElement element in items.Cast<DataElement>())
                {
                    if (element.Specification == null)
                        element.NewSpecification();
                    element.Specification.GroupId = groupId;

                    Add(element);
                }
            }

            return this;
        }


        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the identifier key.
        /// </summary>
        public override string Key()
        {
            return this.Id;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The output log.</param>
        /// <returns>True if this instance has been</returns>
        public bool SaveXml(string filePath, IBdoLog log = null)
        {
            UpdateStorageInfo(log);

            if (XmlHelper.SaveXml(this, filePath, log))
            {
                CurrentFilePath = filePath;
                return true;
            }

            return false;
        }

        #endregion

        // ------------------------------------------
        // IDescribedDataItem IMPLEMENTATION
        // ------------------------------------------

        #region IDescribedDataItem Implementation

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddDescription(string text)
        {
            AddDescription("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddDescription(string key, string text)
        {
            (Description ?? (Description = new DictionaryDataItem())).Add(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetDescription(string text)
        {
            SetDescription("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetDescription(string key = "*", string text = "*")
        {
            (Description ?? (Description = new DictionaryDataItem())).Set(key, text);
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public virtual string GetDescription(string variantName = "*", string defaultVariantName = "*")
        {
            if (Description == null) return "";
            string label = Description.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = Description.GetContent(defaultVariantName);
            return label ?? "";
        }

        #endregion
    }
}
