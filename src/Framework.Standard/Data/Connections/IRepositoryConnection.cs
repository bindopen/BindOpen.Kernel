using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Standard.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Standard.Data.Connections
{

    /// <summary>
    /// This interfaces represents a connection.
    /// </summary>
    interface IRepositoryConnection : IConnection
    {

        // Pull ---------------------------------------

        /// <summary>
        /// Pulls a remote file to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The remote URI to consider.</param>
        /// <param name="localPathUri">The URI of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local file can be overwritten.</param>
        void Pull(
            String remoteFileUri,
            String localPathUri,
            Boolean canOverwrite,
            Log log = null);

        /// <summary>
        /// Pulls a remote files in folder to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The remote URI to consider.</param>
        /// <param name="remoteFilter">The remote filter to consider.</param>
        /// <param name="localPathUri">The URI of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        void Pull(
            String remoteFileUri,
            String remoteFilter,
            String localPathUri,
            Boolean canOverwrite,
            Log log = null,
            Boolean isRecursive = false);

        /// <summary>
        /// Pulls remote files to a local URI.
        /// </summary>
        /// <param name="remoteFileUris">The remote URIs to consider.</param>
        /// <param name="localPathUri">The URI of the local path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        void Pull(
            List<String> remoteFileUris,
            String localPathUri,
            Boolean canOverwrite,
            Log log = null);

        // Push ---------------------------------------

        /// <summary>
        /// Pushes a local file to a remote URI.
        /// </summary>
        /// <param name="localFileUri">The local URI to consider.</param>
        /// <param name="remotePathUri">The URI of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        void Push(
            String localFileUri,
            String remotePathUri,
            Boolean canOverwrite,
            Log log = null);

        /// <summary>
        /// Pushes a local file in folder to a remote URI.
        /// </summary>
        /// <param name="localFileUri">The local URI to consider.</param>
        /// <param name="localFilter">The local filter to consider.</param>
        /// <param name="remotePathUri">The URI of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        void Push(
            String localFileUri,
            String localFilter,
            String remotePathUri,
            Boolean canOverwrite,
            Log log = null,
            Boolean isRecursive = false);

        /// <summary>
        /// Pushes local files to a remote URI.
        /// </summary>
        /// <param name="localFileUris">The local URIs to consider.</param>
        /// <param name="remotePathUri">The URI of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote files can be overwritten.</param>
        void Push(
            List<String> localFileUris,
            String remotePathUri,
            Boolean canOverwrite,
            Log log = null);


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
            String folderUri,
            String filter,
            Boolean isRecursive,
            Log log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any);

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
            String folderUri,
            String filter,
            DateTime timeLimit,
            Boolean isRecursive,
            Log log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any);

    }
}
