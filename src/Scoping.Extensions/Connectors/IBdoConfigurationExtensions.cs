using BindOpen.System.Data.Meta;

namespace BindOpen.System.Scoping.Connectors
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static partial class IBdoConfigurationExtensions
    {
        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static T WithConnectionString<T>(
            this T config,
            string connectionString)
            where T : IBdoConfiguration
        {
            config?.Add(("connectionString", connectionString));
            return config;
        }

        /// <summary>
        /// Creates a new literal exp into auto mode.
        /// </summary>
        /// <param key="text">The script text to consider.</param>
        /// <returns>Returns the script exp.</returns>
        public static string GetConnectionString<T>(
            this T config)
            where T : IBdoConfiguration
        {
            return config?.GetData<string>("connectionString");
        }
    }
}