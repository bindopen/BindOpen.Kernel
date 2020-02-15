using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using BindOpen.Extensions.Carriers;
using BindOpen.Extensions.Connectors;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Connections
{

    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    interface IRepoConnection : IBdoConnection
    {
        new BdoRepoConnector Connector { get; }

        // Pull ---------------------------------------

        /// <summary>
        /// Pulls a remote file to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The URI of the remote file to consider.</param>
        /// <param name="localUri">The local URI to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local file can be overwritten.</param>
        void Pull(
            string remoteFileUri,
            string localUri,
            bool canOverwrite,
            IBdoLog log = null);

        /// <summary>
        /// Pulls a remote files in folder to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The URI of the remote file to consider.</param>
        /// <param name="remoteFilter">The remote filter to consider.</param>
        /// <param name="localFolderUri">The URI of the local folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        void Pull(
            string remoteFileUri,
            string remoteFilter,
            string localFolderUri,
            bool canOverwrite,
            IBdoLog log = null,
            bool isRecursive = false);

        /// <summary>
        /// Pulls remote files to a local URI.
        /// </summary>
        /// <param name="remoteFileUris">The remote URIs to consider.</param>
        /// <param name="localFolderUri">The URI of the local folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        void Pull(
            List<string> remoteFileUris,
            string localFolderUri,
            bool canOverwrite,
            IBdoLog log = null);

        // Push ---------------------------------------

        /// <summary>
        /// Pushes a local file to a remote URI.
        /// </summary>
        /// <param name="localFileUri">The local URI to consider.</param>
        /// <param name="remoteUri">The remote URI to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        void Push(
            string localFileUri,
            string remoteUri,
            bool canOverwrite,
            IBdoLog log = null);

        /// <summary>
        /// Pushes a local file into folder to a remote URI.
        /// </summary>
        /// <param name="localFileUri">The local URI to consider.</param>
        /// <param name="localFilter">The local filter to consider.</param>
        /// <param name="remoteFolderUri">The URI of the remote folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// 
        void Push(
            string localFileUri,
            string localFilter,
            string remoteFolderUri,
            bool canOverwrite,
            IBdoLog log = null,
            bool isRecursive = false);

        /// <summary>
        /// Pushes local files to a remote URI.
        /// </summary>
        /// <param name="localFileUris">The local URIs to consider.</param>
        /// <param name="remoteFolderUri">The URI of the remote folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote files can be overwritten.</param>
        void Push(
            List<string> localFileUris,
            string remoteFolderUri,
            bool canOverwrite,
            IBdoLog log = null);

        // Delete ---------------------------------------

        /// <summary>
        /// Deletes the items.
        /// </summary>
        /// <param name="folderUri">The URI of the folder path to consider.</param>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="timeLimit">The date time limit to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// <param name="fileKind">The kind of elements to consider.</param>
        void DeleteItems(
            string folderUri,
            string filter,
            DateTime timeLimit,
            bool isRecursive,
            IBdoLog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any);


        // Browser ---------------------------------------

        /// <summary>
        /// Gets the list of elements of the remote folder.
        /// </summary>
        /// <param name="folderUri">The URI of the folder path to consider.</param>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// <param name="fileKind">The kind of elements to consider.</param>
        /// <returns>Lists of elements of the remote folder.</returns>
        List<RepositoryItem> GetFiles(
            string folderUri,
            string filter,
            bool isRecursive,
            IBdoLog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any);
    }
}
