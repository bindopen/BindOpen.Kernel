using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Standard.Extensions.Carriers;
using BindOpen.Framework.Standard.Extensions.Connectors;

namespace BindOpen.Framework.Standard.Data.Connections
{
    /// <summary>
    /// This class represents a repository connection.
    /// </summary>
    [XmlType("RepositoryConnection", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "repositoryConnection", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RepositoryConnection : Connection, IRepositoryConnection
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public new RepositoryConnector Connector
        {
            get
            {
                return base.Connector as RepositoryConnector;
            }
            set
            {
                SetConnector(value);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnection class.
        /// </summary>
        public RepositoryConnection() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public RepositoryConnection(RepositoryConnector connector)
        {
            this.Connector = connector;
        }

        #endregion

        // -----------------------------------------------
        // FILE MANAGEMENT
        // -----------------------------------------------

        #region File_Management

        // Pull ---------------------------------------

        /// <summary>
        /// Pulls a remote file to a local Uri.
        /// </summary>
        /// <param name="remoteFileUri">The remote Uri to consider.</param>
        /// <param name="localPathUri">The Uri of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local file can be overwritten.</param>
        public virtual void Pull(
            String remoteFileUri,
            String localPathUri,
            Boolean canOverwrite,
            ILog log = null)
        {
            if (this.Connector != null)
                this.Connector.Pull(remoteFileUri, localPathUri, canOverwrite, log);
        }

        /// <summary>
        /// Pulls a remote files in folder to a local Uri.
        /// </summary>
        /// <param name="remoteFileUri">The remote Uri to consider.</param>
        /// <param name="remoteFilter">The remote filter to consider.</param>
        /// <param name="localPathUri">The Uri of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        public virtual void Pull(
            String remoteFileUri,
            String remoteFilter,
            String localPathUri,
            Boolean canOverwrite,
            ILog log = null,
            Boolean isRecursive = false)
        {
            if (this.Connector != null)
                this.Connector.Pull(remoteFileUri, remoteFilter, localPathUri, canOverwrite, log, isRecursive);
        }

        /// <summary>
        /// Pulls remote files to a local Uri.
        /// </summary>
        /// <param name="remoteFileUris">The remote Uris to consider.</param>
        /// <param name="localPathUri">The Uri of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        public virtual void Pull(
            List<string> remoteFileUris,
            String localPathUri,
            Boolean canOverwrite,
            ILog log = null)
        {
            if (this.Connector != null)
                this.Connector.Pull(remoteFileUris, localPathUri, canOverwrite, log);
        }

        // Push ---------------------------------------

        /// <summary>
        /// Pushes a local file to a remote Uri.
        /// </summary>
        /// <param name="localFileUri">The local Uri to consider.</param>
        /// <param name="remotePathUri">The Uri of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        public virtual void Push(
            String localFileUri,
            String remotePathUri,
            Boolean canOverwrite,
            ILog log = null)
        {
            if (this.Connector != null)
                this.Connector.Push(localFileUri, remotePathUri, canOverwrite, log);
        }

        /// <summary>
        /// Pushes a local file in folder to a remote Uri.
        /// </summary>
        /// <param name="localFileUri">The local Uri to consider.</param>
        /// <param name="localFilter">The local filter to consider.</param>
        /// <param name="remotePathUri">The Uri of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        public virtual void Push(
            String localFileUri,
            String localFilter,
            String remotePathUri,
            Boolean canOverwrite,
            ILog log = null,
            Boolean isRecursive = false)
        {
            if (this.Connector != null)
                this.Connector.Push(localFileUri, localFilter, remotePathUri, canOverwrite, log, isRecursive);
        }

        /// <summary>
        /// Pushes local files to a remote Uri.
        /// </summary>
        /// <param name="localFileUris">The local Uris to consider.</param>
        /// <param name="remotePathUri">The Uri of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote files can be overwritten.</param>
        public virtual void Push(
            List<string> localFileUris,
            String remotePathUri,
            Boolean canOverwrite,
            ILog log = null)
        {
            if (this.Connector != null)
                this.Connector.Pull(localFileUris, remotePathUri, canOverwrite, log);
        }


        // Browser ---------------------------------------

        /// <summary>
        /// Gets the list of elements of the remote folder.
        /// </summary>
        /// <param name="folderUri">The Uri of the folder path to consider.</param>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// <param name="fileKind">The kind of elements to consider.</param>
        /// <returns>Lists of elements of the remote folder.</returns>
        public virtual List<RepositoryItem> GetFiles(
            String folderUri,
            String filter,
            Boolean isRecursive,
            ILog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
            List<RepositoryItem> repositoryItems = new List<RepositoryItem>();

            if (this.Connector != null)
                repositoryItems = this.Connector.GetFiles(folderUri, filter, isRecursive, log, fileKind);

            return repositoryItems;
        }

        /// <summary>
        /// Deletes the items.
        /// </summary>
        /// <param name="folderUri">The Uri of the folder path to consider.</param>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="timeLimit">The date time limit to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// <param name="fileKind">The kind of elements to consider.</param>
        public virtual void DeleteItems(
            String folderUri,
            String filter,
            DateTime timeLimit,
            Boolean isRecursive,
            ILog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
            if (this.Connector != null)
                this.Connector.DeleteItems(folderUri, filter, timeLimit, isRecursive, log, fileKind);
        }

        #endregion
    }
}
