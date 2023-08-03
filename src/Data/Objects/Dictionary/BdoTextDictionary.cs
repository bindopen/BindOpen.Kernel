using BindOpen.System.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a dico data item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public class BdoTextDictionary : Dictionary<string, string>, IBdoTextDictionary
    {
        // --------------------------------------------------
        // Converters
        // --------------------------------------------------

        #region Converters

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoTextDictionary((string Key, string Value)[] items)
        {
            var dico = BdoData.NewDictionary();
            foreach (var (Key, Value) in items)
            {
                dico.Add(Key, Value);
            }

            return dico;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoTextDictionary(string text)
        {
            var dico = BdoData.NewDictionary();
            dico.With(StringHelper.__Star, text);

            return dico;
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class. 
        /// </summary>
        public BdoTextDictionary()
        {
        }

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified Implementation

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // --------------------------------------------------
        // IBdoDictionary Implementation
        // --------------------------------------------------

        #region IBdoDictionary

        /// <summary>
        /// Text of the specified key.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        public new string this[string key]
        {
            get { return GetValue(key); }
            set { Add(key, value); }
        }

        /// <summary>
        /// Text of the specified key.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <param key="defaultKey">The default key to consider.</param>
        /// <returns>Returns the specified text.</returns>
        public string this[string key, string defaultKey]
        {
            get { return GetValue(key, defaultKey); }
        }

        /// <summary>
        /// Adds a new value to this instance with the specified key and text.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <param key="text">The text to consider.</param>
        /// <param key="availableKeys">The available keys to consider.</param>
        /// <returns>Returns the added data key value.</returns>
        public new IBdoTextDictionary Add(string key, string text)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text)) return default;

            Remove(key);
            base.Add(key, text);

            return this;
        }

        // Remove -------------------------------

        /// <summary>
        /// Removes the value with the specified key.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        public void Remove(params string[] keys)
        {
            foreach (var key in keys)
            {
                base.Remove(key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="alternateKey"></param>
        /// <returns></returns>
        public string Get(string key = StringHelper.__Star, string alternateKey = null)
        {
            if (key == null) return default;

            return this[key, alternateKey];
        }

        /// <summary>
        /// Indicates whether this intance equals the specified dico.
        /// </summary>
        /// <param key="dico">The dictionar to consider.</param>
        /// <returns>Returns true if this instance equals the specified dico. False otherwise.</returns>
        public bool Equals(IBdoTextDictionary dico)
        {
            if (dico == null || Values == null || dico.Values == null || Values.Count != dico.Values.Count)
            {
                return false;
            }

            var isEqual = true;
            foreach (var pair in this)
            {
                isEqual &= pair.Value == dico[pair.Key];
            }

            return isEqual;
        }

        // Keys -------------------------------

        /// <summary>
        /// Returns the culture information sets.
        /// </summary>
        /// <returns>Returns information about all the UI cultures.</returns>
        public static IEnumerable<CultureInfo> GetCultureInfoItems()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        }

        // Text -------------------------------

        /// <summary>
        /// Returns the content corresponding to the specified key.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <param key="alternateKey">The alternate key to used if the key is not found.</param>
        /// <returns>Returns the text corresponding to the specified user interface language ID.
        /// Returns empty if there is none.</returns>
        private string GetValue(string key = StringHelper.__Star, string alternateKey = null)
        {
            TryGetValue(key, out string value);
            if (value != null)
            {
                return value;
            }
            else if (alternateKey != null)
            {
                return GetValue(alternateKey);
            }

            return null;
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
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <param key="areas">The areas to consider.</param>
        /// <returns>Returns a cloned instance.</returns>
        public T Clone<T>() where T : class
        {
            return Clone() as T;
        }

        #endregion

        // --------------------------------------------------
        // IDisposable Implementation
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        #endregion
    }

}
