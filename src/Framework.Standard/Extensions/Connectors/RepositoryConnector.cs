using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Standard.Extensions.Carriers;

namespace BindOpen.Framework.Standard.Extensions.Connectors
{
    /// <summary>
    /// This class represents a repository connector.
    /// </summary>
    public class RepositoryConnector : Connector
    {
        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector class.
        /// </summary>
        protected RepositoryConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected RepositoryConnector(IConnectorDto dto)
        {
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
            log = log ?? new Log();

            List<RepositoryItem> files = this.GetFiles(
                remoteFileUri,
                remoteFilter,
                isRecursive,
                log,
                CarrierKind_standard.File);
            foreach (RepositoryItem file in files)
                this.Pull(file.Dto?.Path, localPathUri, canOverwrite, log);
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
            log = log ?? new Log();

            foreach (String remoteFileUri in remoteFileUris)
                this.Pull(remoteFileUri, localPathUri, canOverwrite, log);
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
            log = log ?? new Log();

            List<RepositoryItem> files = this.GetFiles(
                localFileUri,
                localFilter,
                isRecursive,
                log,
                CarrierKind_standard.File);
            foreach (RepositoryItem file in files)
                this.Pull(file.Dto?.Path, remotePathUri, canOverwrite, log);
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
            log = log ?? new Log();

            foreach (String remoteFileUri in localFileUris)
                this.Pull(remoteFileUri, remotePathUri, canOverwrite, log);
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
            String folderUri,
            String filter,
            DateTime timeLimit,
            Boolean isRecursive,
            ILog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
        }

        #endregion
    }
}
