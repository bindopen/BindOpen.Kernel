using BindOpen.Data.Elements;
using System.Linq;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configurations.
    /// </summary>
    public static partial class ConfigurationFactory
    {
        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoUsableConfiguration CreateUsable(string filePath, params DataElement[] items)
        {
            return CreateUsable(filePath, null, items);
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T CreateUsable<T>(string filePath, params DataElement[] items)
            where T : BdoUsableConfiguration, new()
        {
            return CreateUsable<T>(filePath, null, items);
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoUsableConfiguration CreateUsable(
            string filePath,
            string[] usingFilePaths,
            params DataElement[] items)
        {
            return CreateUsable<BdoUsableConfiguration>(filePath, usingFilePaths, items);
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T CreateUsable<T>(
            string filePath,
            string[] usingFilePaths,
            params DataElement[] items)
            where T : BdoUsableConfiguration, new()
        {
            var config = Create<T>(filePath, items);
            config.UsingFilePaths = usingFilePaths?.ToList();
            return config;
        }
    }
}
