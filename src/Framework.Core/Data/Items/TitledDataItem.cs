﻿using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents titled data item.
    /// </summary>
    [Serializable()]
    [XmlType("TitledDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("titledDataItem", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TitledDataItem : NamedDataItem, ITitledDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Title of this instance.
        /// </summary>
        [XmlElement("title", typeof(DictionaryDataItem))]
        public DictionaryDataItem Title { get; set; } = null;

        /// <summary>
        /// Specification of the Title property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool TitleSpecified => Title != null && (Title.AvailableKeysSpecified || Title.ValuesSpecified || Title.SingleValueSpecified);

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
        public TitledDataItem(string name,
            string namePreffix = "",
            string id = null)
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
            string name,
            string title,
            string namePreffix = "",
            string id = null)
            : base(name, namePreffix, id)
        {
            if (title != null)
                Title = new DictionaryDataItem(title);
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
        public void Update(ITitledDataItem updateBaseObject)
        {
            if (updateBaseObject != null)
            {
                if (updateBaseObject.Title != null)
                    Title = new DictionaryDataItem(updateBaseObject.Title);
            }
        }

        // Title -------------------------------

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void AddTitle(string text)
        {
            AddTitle("*", text);
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void AddTitle(string key, string text)
        {
            (Title ?? (Title = new DictionaryDataItem())).AddValue(key, text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public void SetTitle(string text)
        {
            SetTitle("*", text);
        }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public void SetTitle(string key = "*", string text = "*")
        {
            (Title ?? (Title = new DictionaryDataItem())).SetValue(key, text);
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
        public virtual string GetTitle(string variantName = "*", string defaultVariantName = "*")
        {
            if (Title == null) return "";

            string label = Title.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
            {
                label = Title.GetContent(defaultVariantName);
            }
            if (string.IsNullOrEmpty(label))
            {
                label = Name;
            }

            return label ?? "";
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            TitledDataItem item = base.Clone() as TitledDataItem;
            if (Title != null)
                item.Title = Title.Clone() as DictionaryDataItem;

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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);
            Title?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            Title?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
        }

        #endregion
    }
}
