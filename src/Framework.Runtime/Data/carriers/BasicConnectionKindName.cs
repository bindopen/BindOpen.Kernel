
namespace cor_base_wdl.data.connectors
{
    /// <summary>
    /// This enumeration lists all the possible names of connector.
    /// </summary>
    public enum BasicConnectorDefinitionName
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
        /// Local.
        /// </summary>
        FileLocal,
        /// <summary>
        /// File remote.
        /// </summary>
        FileRemote,
        /// <summary>
        /// Ftp.
        /// </summary>
        Ftp,
        /// <summary>
        /// SFtp.
        /// </summary>
        SFtp,
        /// <summary>
        /// Scp.
        /// </summary>
        Scp,
        /// <summary>
        /// Pop3.
        /// </summary>
        Pop3,
        /// <summary>
        /// Smtp.
        /// </summary>
        Smtp,
        /// <summary>
        /// Imap.
        /// </summary>
        Imap,
        /// <summary>
        /// OLE DB.
        /// </summary>
        OleDb,
        /// <summary>
        /// SQL client.
        /// </summary>
        SqlClient,
        /// <summary>
        /// SQL client for entity.
        /// </summary>
        SqlClientEntity
    }
}
