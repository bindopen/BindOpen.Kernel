using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.Data.Connections;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This class represents a repository connector.
    /// </summary>
    public abstract class BdoRepoConnector : BdoConnector
    {
        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector class.
        /// </summary>
        protected BdoRepoConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected BdoRepoConnector(IBdoConnectorConfiguration dto) : base(dto)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public new abstract BdoRepoConnection CreateConnection(IBdoLog log = null);

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
            string remoteFileUri,
            string localPathUri,
            Boolean canOverwrite,
            IBdoLog log = null)
        {
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
            string remoteFileUri,
            string remoteFilter,
            string localPathUri,
            Boolean canOverwrite,
            IBdoLog log = null,
            Boolean isRecursive = false)
        {
            List<RepositoryItem> files = GetFiles(
                remoteFileUri,
                remoteFilter,
                isRecursive,
                log,
                CarrierKind_standard.File);
            foreach (RepositoryItem file in files)
                Pull(file.Path, localPathUri, canOverwrite, log);
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
            string localPathUri,
            Boolean canOverwrite,
            IBdoLog log = null)
        {
            foreach (string remoteFileUri in remoteFileUris)
                Pull(remoteFileUri, localPathUri, canOverwrite, log);
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
            string localFileUri,
            string remotePathUri,
            Boolean canOverwrite,
            IBdoLog log = null)
        {
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
            string localFileUri,
            string localFilter,
            string remotePathUri,
            Boolean canOverwrite,
            IBdoLog log = null,
            Boolean isRecursive = false)
        {
            List<RepositoryItem> files = GetFiles(
                localFileUri,
                localFilter,
                isRecursive,
                log,
                CarrierKind_standard.File);
            foreach (RepositoryItem file in files)
                Pull(file.Path, remotePathUri, canOverwrite, log);
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
            string remotePathUri,
            Boolean canOverwrite,
            IBdoLog log = null)
        {
            foreach (string remoteFileUri in localFileUris)
                Pull(remoteFileUri, remotePathUri, canOverwrite, log);
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
            string folderUri,
            string filter,
            Boolean isRecursive,
            IBdoLog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
            return new List<RepositoryItem>();
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
            string folderUri,
            string filter,
            DateTime timeLimit,
            Boolean isRecursive,
            IBdoLog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
        }

        #endregion
    }
}
