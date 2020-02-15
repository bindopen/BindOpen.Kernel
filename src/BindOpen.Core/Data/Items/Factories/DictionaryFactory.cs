using System;
using System.Data;
using System.Reflection;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a dictionary data factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static class DictionaryFactory
    {
        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static DictionaryDataItem Create(params (string name, string value)[] items)
        {
            DictionaryDataItem dictionary = new DictionaryDataItem();
            foreach (var (name, value) in items)
            {
                dictionary.AddValue(name, value);
            }

            return dictionary;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static DictionaryDataItem Create(params IDataKeyValue[] values)
        {
            DictionaryDataItem item = new DictionaryDataItem();
            foreach (DataKeyValue value in values)
            {
                if (value != null)
                {
                    item.AddValue(value.Key, value.Content);
                }
            }

            return item;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from a global object/text data row.
        /// </summary>
        /// <param name="dataRow">The global object/text row to consider.</param>
        /// <param name="uiCultureNames">The UI culture names to consider.</param>
        public static DictionaryDataItem Create(DataRow dataRow, string[] uiCultureNames)
        {
            DictionaryDataItem item = new DictionaryDataItem();
            if (dataRow != null)
            {
                foreach (string stringObject in uiCultureNames)
                {
                    if ((!dataRow.IsNull(stringObject)) || (dataRow[stringObject] != DBNull.Value))
                    {
                        item.AddValue(stringObject.ToLower(), dataRow[stringObject.ToUpper()].ToString());
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from an object.
        /// </summary>
        /// <param name="aObject">The object to consider.</param>
        /// <param name="mappings">The mappings to consider.</param>
        public static DictionaryDataItem Create(object aObject, (string from, string to)[] mappings)
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

                        item.AddValue(propertyName, propertyValue);
                    }
                }
            }

            return item;
        }
    }
}
