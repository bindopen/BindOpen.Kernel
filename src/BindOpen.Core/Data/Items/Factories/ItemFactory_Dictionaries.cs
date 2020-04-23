using System.Reflection;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class ItemFactory
    {
        // Dictionaries -----------------------------

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// specifying the text for the default key.
        /// </summary>
        /// <param name="text">The text to consider</param>
        public static DictionaryDataItem CreateDictionary(string text)
        {
            DictionaryDataItem dictionary = new DictionaryDataItem();
            dictionary.Set("*", text);

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// specifying the text for the default user interface language ID.
        /// </summary>
        /// <param name="key">The variant name to consider.</param>
        /// <param name="text">The text to consider.</param>
        public static DictionaryDataItem CreateDictionary(string key, string text)
        {
            DictionaryDataItem dictionary = new DictionaryDataItem();
            dictionary.Set(key, text);

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static DictionaryDataItem CreateDictionary(params (string Key, string Value)[] items)
        {
            DictionaryDataItem dictionary = new DictionaryDataItem();
            foreach (var (key, value) in items)
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static DictionaryDataItem CreateDictionary(params IDataKeyValue[] values)
        {
            DictionaryDataItem item = new DictionaryDataItem();
            item.Set(values);

            return item;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from an object.
        /// </summary>
        /// <param name="aObject">The object to consider.</param>
        /// <param name="mappings">The mappings to consider.</param>
        public static DictionaryDataItem CreateDictionaryFromObject(object aObject, (string from, string to)[] mappings)
        {
            DictionaryDataItem item = new DictionaryDataItem();
            if (aObject != null)
            {
                foreach (PropertyInfo updatePropertyInfo in aObject.GetType().GetProperties())
                {
                    if (updatePropertyInfo.PropertyType == typeof(string))
                    {
                        string propertyName = updatePropertyInfo.Name;
                        string propertyValue = updatePropertyInfo.GetValue(aObject)?.ToString();

                        item.Add(propertyName, propertyValue);
                    }
                }
            }

            return item;
        }
    }
}
