using BindOpen.Extensions.Common;
using System;

namespace BindOpen.Extensions.Connectors
{

    /// <summary>
    /// This enumeration lists all the possible kinds of the 'Standard' connectors.
    /// </summary>
    public enum ConnectorKind_standard
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
        Local,

        /// <summary>
        /// Remote.
        /// </summary>
        Remote,

        /// <summary>
        /// FTP.
        /// </summary>
        Ftp,

        /// <summary>
        /// SCP.
        /// </summary>
        Scp,

        /// <summary>
        /// SFTP.
        /// </summary>
        SFtp
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the ConnectorKind_standard enumeration.
    /// </summary>
    public static class ConnectorKind_standardExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified connector kind.
        /// </summary>
        /// <param name="connectorKind_standard">The connector kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this ConnectorKind_standard connectorKind_standard)
        {
            return connectorKind_standard.ToString().ToLower().GetUniqueName_standard();
        }
    }

    #endregion

}
