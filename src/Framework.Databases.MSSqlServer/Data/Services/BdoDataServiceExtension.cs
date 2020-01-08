using BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors;

namespace BindOpen.Framework.Core.Application.Services.Data
{
    /// <summary>
    /// This class represents a data service extension.
    /// </summary>
    public static partial class BdoDataServiceExtension
    {
        /// <summary>
        /// Uses PostgreSql.
        /// </summary>
        /// <param name="dataService">The data service to consider.</param>
        /// <typeparam name="T">The class of the data to consider.</typeparam>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns the specified data.</returns>
        public static T UsePostgreSql<T>(this T dataService, string connectionString) where T : IBdoDataService
        {
            if (dataService != null)
            {
                dataService.SetConnection(new DatabaseConnector_MSSqlServer(connectionString)?.CreateConnection());
            }

            return dataService;
        }
    }
}
