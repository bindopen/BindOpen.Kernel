﻿using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    [XmlType("Configuration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("config", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class BaseConfiguration : DataElementSet, IBaseConfiguration
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
        /// Specification of the Description property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DescriptionSpecified => Description != null && (Description.AvailableKeysSpecified || Description.ValuesSpecified || Description.SingleValueSpecified);

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
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        public BaseConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BaseConfiguration(params IDataElement[] items)
            : base(items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BaseConfiguration(string filePath, params IDataElement[] items)
            : base(items)
        {
            CurrentFilePath = filePath;
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
        public IBaseConfiguration AddGroup(string groupId, params IDataElement[] items)
        {
            if (items!=null)
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The output log.</param>
        /// <returns>True if this instance has been</returns>
        public bool SaveXml(string filePath, ILog log = null)
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
            (Description ?? (Description = new DictionaryDataItem())).AddValue(key, text);
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
            (Description ?? (Description = new DictionaryDataItem())).SetValue(key, text);
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