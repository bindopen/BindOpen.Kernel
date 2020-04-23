using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static partial class ConfigurationFactory
    {
        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T Create<T>(params DataElement[] items)
            where T : BdoBaseConfiguration, new()
        {
            return Create<T>(null, items);
        }

        /// <summary>
        /// Instantiates a new instance of the Configuration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T Create<T>(string filePath, params DataElement[] items)
            where T : BdoBaseConfiguration, new()
        {
            var config = ItemFactory.CreateSet<T, IDataElement>(items);
            config.CurrentFilePath = filePath;
            return config;
        }
    }
}
