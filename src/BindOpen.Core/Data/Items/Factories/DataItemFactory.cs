using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static class DataItemFactory
    {
        // Data sources -----------------------------

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        public static Datasource CreateDatasource(
                string name,
                DatasourceKind kind)
        {
            var datasource = new Datasource(name)
            {
                Kind = kind
            };

            return datasource;
        }

        // Dictionaries -----------------------------

        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class specifying the values.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static DictionaryDataItem CreateDictionary(params (string name, string value)[] items)
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
        public static DictionaryDataItem CreateDictionary(params IDataKeyValue[] values)
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
        public static DictionaryDataItem CreateDictionary(DataRow dataRow, string[] uiCultureNames)
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
        public static DictionaryDataItem CreateDictionary(object aObject, (string from, string to)[] mappings)
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

        // Sets -----------------------------

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="Q">The data item set type to consider.</typeparam>
        /// <typeparam name="T">The identified data item to consider.</typeparam>
        public static Q CreateSet<Q, T>(params T[] items)
            where Q : DataItemSet<T>, new()
            where T : IIdentifiedDataItem
        {
            return new Q()
            {
                Items = items?.ToList()
            };
        }

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <typeparam name="T">The class of the named data items.</typeparam>
        public static DataItemSet<T> CreateItemSet<T>(params T[] items) where T : IIdentifiedDataItem
        {
            return CreateSet<DataItemSet<T>, T>(items);
        }
    }
}
