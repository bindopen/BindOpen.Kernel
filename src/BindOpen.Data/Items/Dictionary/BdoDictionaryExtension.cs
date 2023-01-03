using System.Reflection;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a dictionary data item extension.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static class BdoDictionaryExtension
    {
        /// <summary>
        /// Instantiates a new instance of the DictionaryDataItem class
        /// from an object.
        /// </summary>
        /// <param name="obj">The object to consider.</param>
        /// <param name="mappings">The mappings to consider.</param>
        public static BdoDictionary AsDictionary(
            this object obj,
            (string from, string to)[] mappings)
        {
            var dictionary = new BdoDictionary();
            if (obj != null)
            {
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                {
                    if (info.PropertyType == typeof(string))
                    {
                        string propertyName = info.Name;
                        string propertyValue = info.GetValue(obj)?.ToString();

                        dictionary.Add(propertyName, propertyValue);
                    }
                }
            }

            return dictionary;
        }
    }
}
