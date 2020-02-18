using BindOpen.Databases.Extensions;

namespace BindOpen.Extensions.Connectors
{

    /// <summary>
    /// This enumeration lists all the possible kinds of database connectors.
    /// </summary>
    public enum BdoDbConnectorKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Microsoft SQL Server.
        /// </summary>
        MSSqlServer,

        /// <summary>
        /// Oracle.
        /// </summary>
        Oracle,

        /// <summary>
        /// MySQL.
        /// </summary>
        MySQL,

        /// <summary>
        /// Postgre SQL.
        /// </summary>
        PostgreSql
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the ConnectorKind_database enumeration.
    /// </summary>
    public static class ConnectorKind_databaseExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified connector kind.
        /// </summary>
        /// <param name="connectorKind_database">The connector kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this BdoDbConnectorKind connectorKind_database)
        {
            return connectorKind_database.ToString().ToLower().GetUniqueName_database();
        }
    }

    #endregion


}
