using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a dictionary data item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="DataKeyValue"/>
    [XmlType("DictionaryDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "item", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DictionaryDataItem : DataItem, IDictionaryDataItem
    {
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
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The single '*' value to consider.
        /// </summary>
        [XmlText()]
        public string SingleValue
        {
            get
            {
                return GetContent();
            }
            set
            {
                Set(value);
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
                _availableKeys = value.Select(p => p.ToLower()).ToList();
                Update<DictionaryDataItem>();
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
            set
            {
                _values = new List<DataKeyValue>(value);
                Update<DictionaryDataItem>();
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
            set { Add(key, value); }
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

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        // Add -----------------------------

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param name="values">The value to add.</param>
        public IDictionaryDataItem Add(params IDataKeyValue[] values)
        {
            foreach (var value in values)
            {
                Add(value.Key, value.Content);
            }

            return this;
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public IDictionaryDataItem Add(string text)
        {
            return Add("*", text);
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public IDictionaryDataItem Add(string key, string text)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text)) return null;

            Remove(key);
            if (_availableKeys == null || _availableKeys.Count == 0 || AvailableKeys.Contains(key.ToLower()))
            {
                _values.Add(new DataKeyValue(key, text));
            }

            return this;
        }

        /// <summary>
        /// Adds a new value to this instance.
        /// </summary>
        /// <param name="values">The value to add.</param>
        public IDictionaryDataItem Set(params IDataKeyValue[] values)
        {
            Clear();
            Add(values);

            return this;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param name="text">The text of the value to add.</param>
        public IDictionaryDataItem Set(string text)
        {
            Set("*", text);

            return this;
        }

        /// <summary>
        /// Sets the text of the default value.
        /// </summary>
        /// <param name="key">The key of the value to add.</param>
        /// <param name="text">The text of the value to add.</param>
        public IDictionaryDataItem Set(string key, string text)
        {
            Clear();
            Add(text);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IIdentifiedDataItem WithId(string id)
        {
            Id = id;

            return this;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public IDictionaryDataItem Clear()
        {
            _values = new List<DataKeyValue>();

            return this;
        }

        // Remove -------------------------------

        /// <summary>
        /// Removes the value with the specified key.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        public IDictionaryDataItem Remove(params string[] keys)
        {
            foreach (var key in keys)
            {
                Values.RemoveAll(p => key.KeyEquals(p));
            }

            return this;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this intance equals the specified dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionar to consider.</param>
        /// <returns>Returns true if this instance equals the specified dictionary. False otherwise.</returns>
        public bool Equals(DictionaryDataItem dictionary)
        {
            if (dictionary == null || Values.Count != dictionary.Values.Count)
            {
                return false;
            }

            var isEqual = true;
            foreach (var value in Values)
            {
                isEqual &= value.Content == dictionary[value.Key];
            }

            return isEqual;
        }

        /// <summary>
        /// The key of this instance.
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return Id;
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static implicit operator DictionaryDataItem(string st)
        {
            return ItemFactory.CreateDictionary(st);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="item">The dictionary data item to consider.</param>
        public static implicit operator string(DictionaryDataItem item)
        {
            return item?.GetContent();
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
        public string GetContent(string key = "*", string alternateKey = null)
        {
            DataKeyValue dataKeyValue = GetValue(key);
            if (dataKeyValue != null)
                return dataKeyValue.Content;
            else if (alternateKey != null)
                return GetContent(alternateKey);
            return string.Empty;
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            if (_availableKeys?.Count > 0)
                _values.RemoveAll(p => p.Key == null || !_availableKeys.Contains(p.Key.ToLower()));

            return new BdoLog();
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
            string st = string.Empty;

            st += indent + nodeName + "\n";
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
        public override object Clone(params string[] areas)
        {
            DictionaryDataItem item = base.Clone(areas) as DictionaryDataItem;

            item._availableKeys = new List<string>(_availableKeys);
            item._values = new List<DataKeyValue>();
            foreach (DataKeyValue dataKeyValue in _values)
            {
                item.Add(dataKeyValue.Clone() as DataKeyValue);
            }

            return item;
        }

        #endregion
    }

}
