using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Dictionary
{
    /// <summary>
    /// This class represents a dictionary data item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="DataKeyValue"/>
    [Serializable()]
    [XmlType("DictionaryDataItem", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "item", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DictionaryDataItem : DataItem, IDictionaryDataItem
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        private readonly string[] __UICultureNames = new string[] { "de", "du", "en", "es", "fr", "it", "po" };

        #endregion

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private List<DataKeyValue> _values = new List<DataKeyValue>();
        private List<string> _availableKeys = new List<string>();

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The single '*' value to consider.
        /// </summary>
        [XmlText()]
        public string SingleValue
        {
            get {
                return GetContent();
            }
            set {
                SetValue(value);
            }
        }

        /// <summary>
        /// Specification of the SingleValue property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool SingleValueSpecified
        {
            get
            {
                return _values != null && _values.Count ==1 && _values[0].Key=="*" && !AvailableKeysSpecified ;
            }
        }

        /// <summary>
        /// Available keys of this instance.
        /// </summary>
        [XmlIgnore()]
        public virtual List<string> AvailableKeys
        {
            get { return _availableKeys; }
            set
            {
                _availableKeys = value.Select(p=> p.ToLower()).ToList();
                Update<DictionaryDataItem>();
            }
        }

        /// <summary>
        /// Specification of the AvailableKeys property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool AvailableKeysSpecified
        {
            get
            {
                return _availableKeys?.Count > 0;
            }
        }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [DataMember(Name = "values")]
        [XmlElement("add.value")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual List<DataKeyValue> Values
        {
            get { return _values; }
            set {
                _values=new List<DataKeyValue>(value);
                Update<DictionaryDataItem>();
            }
        }

        /// <summary>
        /// Specification of the Values property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ValuesSpecified
        {
            get
            {
                return _values != null && _values.Count > 0 && !SingleValueSpecified;
            }
        }

        /// <summary>
        /// Text of the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        [XmlIgnore()]
        public string this[string key]
        {
            get { return GetContent(key); }
            set { AddValue(key, value); }
        }

        /// <summary>
        /// Text of the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        public string this[string key, string defaultKey]
        {
            get { return GetContent(key, defaultKey); }
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class. 
        /// </summary>
        public DictionaryDataItem()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// specifying the text for the default key.
        /// </summary>
        /// <param name="text">The text to consider</param>
        public DictionaryDataItem(string text)
        {
            AddValue("*", text);
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public DictionaryDataItem(params IDataKeyValue[] values)
        {
            foreach(DataKeyValue value in values)
            {
                if (value != null)
                {
                    AddValue(value.Key, value.Content);
                }
            }
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// specifying the text for the default user interface language ID.
        /// </summary>
        /// <param name="key">The variant name to consider.</param>
        /// <param name="text">The text to consider.</param>
        public DictionaryDataItem(string key, string text)
        {
            AddValue(key, text);
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from a global object/text data row.
        /// </summary>
        /// <param name="dataRow">The global object/text row to consider.</param>
        public DictionaryDataItem(DataRow dataRow)
        {
            if (dataRow != null)
            {
                foreach (string stringObject in __UICultureNames)
                {
                    if ((!dataRow.IsNull(stringObject)) || (dataRow[stringObject] != DBNull.Value))
                    {
                        AddValue(stringObject.ToLower(), dataRow[stringObject.ToUpper()].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from an object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        public DictionaryDataItem(object object1)
        {
            if (object1 != null)
            {
                foreach (string stringObject in __UICultureNames)
                {
                    try
                    {
                        PropertyInfo propertyInfo = object1.GetType().GetProperty(stringObject);
                        if (propertyInfo != null)
                        {
                            string value = (string)propertyInfo.GetValue(object1, null);
                            AddValue(stringObject.ToLower(), value);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        // Add -----------------------------

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param name="dataKeyValue">The value to add.</param>
        public void AddValue(IDataKeyValue dataKeyValue)
        {
            if (dataKeyValue != null)
                AddValue(dataKeyValue.Key, dataKeyValue.Content);
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public IDataKeyValue AddValue(string text)
        {
            return AddValue("*", text);
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public IDataKeyValue AddValue(string key, string text)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text)) return null;

            RemoveValue(key);

            DataKeyValue dataKeyValue = null;
            if (_availableKeys==null || _availableKeys.Count == 0 || AvailableKeys.Contains(key.ToLower()))
                _values.Add(dataKeyValue = new DataKeyValue(key, text));

            return dataKeyValue;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param name="text">The text of the value to add.</param>
        public void SetValue(string text)
        {
            SetValue("*", text);
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param name="key">The key of the value to add.</param>
        /// <param name="text">The text of the value to add.</param>
        public void SetValue(string key, string text)
        {
            Clear();
            AddValue(text);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _values= new List<DataKeyValue>();
        }

        // Remove -------------------------------

        /// <summary>
        /// Removes the value with the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        public void RemoveValue(string key)
        {
            if (key == null) return;

            Values.RemoveAll(p => key.KeyEquals(p));
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="dataKeyValue">The value to remove.</param>
        public void RemoveValue(IDataKeyValue dataKeyValue)
        {
            if (dataKeyValue == null) return;
            Values.RemoveAll(p => dataKeyValue.KeyEquals(p));
        }

        /// <summary>
        /// Removes the values of this instance whose keys are not in the specified list.
        /// </summary>
        /// <param name="keys">The keys to consider.</param>
        public void RemoveValues(List<string> keys)
        {
            Values.RemoveAll(p => keys?.Contains(p.Key) != false);
        }

        // Keys -------------------------------

        /// <summary>
        /// Updates the key of a global value.
        /// </summary>
        /// <param name="oldKey">The old name of the global value.</param>
        /// <param name="newKey">The new name of the global value.</param>
        public void UpdateKey(string oldKey, string newKey)
        {
            DataKeyValue dataKeyValue = GetValue(oldKey);
            if (dataKeyValue != null) dataKeyValue.Key = newKey;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static implicit operator DictionaryDataItem(string st)
        {
            return new DictionaryDataItem(st);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="item">The dictionary data item to consider.</param>
        public static implicit operator string(DictionaryDataItem item)
        {
            return item?.GetContent();
        }

        /// <summary>
        /// Creates a new instance of the DictionaryDataItem class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static DictionaryDataItem Create(string stringObject)
        {
            DictionaryDataItem item = new DictionaryDataItem();
            if (stringObject != null)
                item.Values = stringObject.GetKeyValues();

            return item;
        }

        // Values -------------------------------

        private DataKeyValue GetValue(string key)
        {
            if (key == null) return null;

            return _values.Find(p => p.KeyEquals(key));
        }

        // Keys -------------------------------

        /// <summary>
        /// Returns the culture information sets.
        /// </summary>
        /// <returns>Returns information about all the UI cultures.</returns>
        public static List<CultureInfo> GetCultureInfoItems()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        }

        /// <summary>
        /// Gets the distinct keys of this instance.
        /// </summary>
        /// <returns>Returns the distinct keys of this instance.</returns>
        public List<string> GetDistinctKeys()
        {
            return Values.Select(p => p.Key).Distinct().ToList();
        }

        /// <summary>
        /// Indicates whether this instance has a value for the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns True if this instance has a value for the specified key.</returns>
        public bool HasKey(string key)
        {
            if (key == null) return false;

            return _values.Any(p => p.Key.KeyEquals(key));
        }

        // Text -------------------------------

        /// <summary>
        /// Returns the content corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="alternateKey">The alternate key to used if the key is not found.</param>
        /// <returns>Returns the text corresponding to the specified user interface language ID.
        /// Returns empty if there is none.</returns>
        public string GetContent(string key = "*", string alternateKey=null)
        {
            DataKeyValue dataKeyValue = GetValue(key);
            if (dataKeyValue != null)
                return dataKeyValue.Content;
            else if (alternateKey != null)
                return GetContent(alternateKey);
            return "";
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            if (_availableKeys?.Count > 0)
                _values.RemoveAll(p => p.Key == null || !_availableKeys.Contains(p.Key.ToLower()));

            return new Log();
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the text node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public string GetTextNode(string nodeName, string indent)
        {
            string st = "";

            st += indent  + nodeName + "\n";
            st += "\t" + indent + nodeName + ":values\n";
            foreach (DataKeyValue value in Values)
                st += value.GetTextNode(nodeName + ":values", "\t\t" + indent);
            return st;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            DictionaryDataItem item = base.Clone() as DictionaryDataItem;
            item._availableKeys = new List<string>(_availableKeys);
            item._values = new List<DataKeyValue>();
            foreach (DataKeyValue dataKeyValue in _values)
                item.AddValue(dataKeyValue.Clone() as DataKeyValue);
            return item;
        }

        #endregion
    }

}
