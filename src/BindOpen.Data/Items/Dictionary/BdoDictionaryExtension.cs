using BindOpen.Data.Items;
using System.Reflection;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a dico data item extension.
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
            var dico = new BdoDictionary();
            if (obj != null)
            {
                foreach (PropertyInfo info in obj.GetType().GetProperties())
                {
                    if (info.PropertyType == typeof(string))
                    {
                        string propertyName = info.Name;
                        string propertyValue = info.GetValue(obj)?.ToString();

                        dico.Add(propertyName, propertyValue);
                    }
                }
            }

            return dico;
        }
    }
}
