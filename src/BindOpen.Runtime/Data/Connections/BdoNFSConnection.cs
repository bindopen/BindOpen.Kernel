using BindOpen.Extensions.Carriers;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BindOpen.Data.Connections
{
    /// <summary>
    /// This class represents a file NFS connector.
    /// </summary>
    public class BdoNFSConnection : BdoRepoConnection
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the NFSConnection class.
        /// </summary>
        public BdoNFSConnection(BdoNFSConnector connector) : base()
        {
            _connector = connector;
        }

        #endregion

        // -----------------------------------------------
        // BdoConnection METHODS
        // -----------------------------------------------

        #region BdoConnection_Methods

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Connect()
        {
            return new BdoLog();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Disconnect()
        {
            return new BdoLog();
        }

        #endregion

        // -----------------------------------------------
        // BdoRepoConnection METHODS
        // -----------------------------------------------

        #region BdoRepoConnection_Methods

        // Pull ---------------------------------------

        /// <summary>
        /// Pulls a remote file to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The URI of the remote file to consider.</param>
        /// <param name="localUri">The local URI to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local file can be overwritten.</param>
        public override void Pull(
            string remoteFileUri,
            string localUri,
            bool canOverwrite,
            IBdoLog log = null)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localUri)))
                    Directory.CreateDirectory(Path.GetDirectoryName(localUri));

                File.Copy(remoteFileUri, localUri, canOverwrite);
            }
            catch (Exception exception)
            {
                IBdoLogEvent logEvent = log?.AddException(exception);
            }
        }

        /// <summary>
        /// Pulls a remote files in folder to a local URI.
        /// </summary>
        /// <param name="remoteFileUri">The URI of the remote file to consider.</param>
        /// <param name="remoteFilter">The remote filter to consider.</param>
        /// <param name="localFolderUri">The URI of the local folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>

        public override void Pull(
            string remoteFileUri,
            string remoteFilter,
            string localFolderUri,
            bool canOverwrite,
            IBdoLog log = null,
            bool isRecursive = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pulls remote files to a local URI.
        /// </summary>
        /// <param name="remoteFileUris">The remote URIs to consider.</param>
        /// <param name="localFolderUri">The URI of the local folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the local files can be overwritten.</param>
        public override void Pull(
            List<string> remoteFileUris,
            string localFolderUri,
            bool canOverwrite,
            IBdoLog log = null)
        {
            throw new NotImplementedException();
        }

        // Push ---------------------------------------

        /// <summary>
        /// Posts a local file to a remote Uri.
        /// </summary>
        /// <param name="localFileUri">The local Uri to consider.</param>
        /// <param name="remotePathUri">The Uri of the remote path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote file can be overwritten.</param>
        public override void Push(
           String localFileUri,
           String remotePathUri,
           Boolean canOverwrite,
            IBdoLog log = null)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(remotePathUri)))
                    Directory.CreateDirectory(Path.GetDirectoryName(remotePathUri));

                File.Copy(localFileUri, remotePathUri, canOverwrite);
            }
            catch (Exception exception)
            {
                log?.AddException(exception);
            }
        }

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
        public override void Push(
            string localFileUri,
            string localFilter,
            string remoteFolderUri,
            bool canOverwrite,
            IBdoLog log = null,
            bool isRecursive = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pushes local files to a remote URI.
        /// </summary>
        /// <param name="localFileUris">The local URIs to consider.</param>
        /// <param name="remoteFolderUri">The URI of the remote folder to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="canOverwrite">Indicates whether the remote files can be overwritten.</param>
        public override void Push(
            List<string> localFileUris,
            string remoteFolderUri,
            bool canOverwrite,
            IBdoLog log = null)
        {
            throw new NotImplementedException();
        }

        // Browser ---------------------------------------

        /// <summary>
        /// Waits for the specified file to be accessible.
        /// </summary>
        /// <param name="path">The path of the file to consider.</param>
        /// <param name="aSecondNumber">The number of seconds to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns true if the file is available. False otherwise.</returns>
        public static bool WaitForFile(
            String path,
            int aSecondNumber = 4,
            IBdoLog log = null)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            DateTime dateTime = DateTime.Now.AddSeconds(aSecondNumber);
            bool isFileAccessible = !File.Exists(path);

            while ((DateTime.Now <= dateTime) && (!isFileAccessible))
            {
                FileStream fileStream = null;
                try
                {
                    FileInfo fileInfo = new FileInfo(path);
                    fileStream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    isFileAccessible = true;
                }
                catch
                {
                    isFileAccessible = false;
                }
                finally
                {
                    fileStream?.Close();
                }
            }

            return isFileAccessible;
        }

        /// <summary>
        /// Gets the list of elements of the remote folder.
        /// </summary>
        /// <param name="folderUri">The Uri of the folder path to consider.</param>
        /// <param name="filter">The filter to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isRecursive">Indicates whether the search is folder recursive.</param>
        /// <param name="fileKind">The kind of files to consider.</param>
        /// <returns>Lists of elements of the remote folder.</returns>
        public override List<RepositoryItem> GetFiles(
           String folderUri,
           String filter,
           Boolean isRecursive,
           IBdoLog log = null,
           CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
            Boolean isRegularExpression = ((!string.IsNullOrEmpty(filter)) && (filter.StartsWith("/")));
            Regex regex = null;

            List<RepositoryItem> files = new List<RepositoryItem>();
            if (Directory.Exists(folderUri))
            {
                if ((fileKind == CarrierKind_standard.File) |
                    (fileKind == CarrierKind_standard.Any))
                {
                    FileInfo[] fileInfos = null;

                    if (!isRegularExpression)
                        fileInfos = (new DirectoryInfo(folderUri)).GetFiles((filter ?? "*.*"), (isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                    else
                    {
                        filter = filter.Substring(1);
                        fileInfos = (new DirectoryInfo(folderUri)).GetFiles("*.*", (isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                        try
                        {
                            regex = new Regex(filter);
                        }
                        catch
                        {
                            isRegularExpression = false;
                        }
                    }

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        Boolean isFound = !isRegularExpression;
                        if ((isRegularExpression) & (regex != null))
                            isFound = regex.IsMatch(fileInfo.Name);

                        if (isFound)
                        {
                            RepositoryFile file = new RepositoryFile()
                            {
                                Name = fileInfo.Name,
                                Path = fileInfo.FullName,
                                CreationDate = fileInfo.CreationTime.ToString(),
                                LastAccessDate = fileInfo.LastAccessTime.ToString(),
                                LastWriteDate = fileInfo.LastWriteTime.ToString(),
                                Length = (ulong)fileInfo.Length,
                                ParentPath = folderUri
                            };
                            files.Add(file);
                        }
                    }
                }
                if ((fileKind == CarrierKind_standard.Folder) |
                    (fileKind == CarrierKind_standard.Any))
                {
                    DirectoryInfo[] directoryInfos = null;

                    if (!isRegularExpression)
                        directoryInfos = (new DirectoryInfo(folderUri)).GetDirectories((filter ?? "*.*"), (isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                    else
                    {
                        filter = filter.Substring(1);
                        directoryInfos = (new DirectoryInfo(folderUri)).GetDirectories("*.*", (isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                        try
                        {
                            regex = new Regex(filter);
                        }
                        catch
                        {
                            isRegularExpression = false;
                        }
                    }

                    foreach (DirectoryInfo directoryInfo in directoryInfos)
                    {
                        Boolean isFound = !isRegularExpression;
                        if (isRegularExpression & (regex != null))
                            isFound = regex.IsMatch(directoryInfo.Name);

                        if (isFound)
                        {
                            RepositoryFolder folder = new RepositoryFolder()
                            {
                                Name = directoryInfo.Name,
                                Path = directoryInfo.FullName,
                                CreationDate = directoryInfo.CreationTime.ToString(),
                                LastAccessDate = directoryInfo.LastAccessTime.ToString(),
                                LastWriteDate = directoryInfo.LastWriteTime.ToString(),
                                ParentPath = folderUri
                            };
                            files.Add(folder);
                        }
                    }
                }
            }

            return files;
        }

        #endregion

        // -----------------------------------------------
        // OTHER METHODS
        // -----------------------------------------------

        #region Other_Methods


        // Delete ---------------------------------------------------

        /// <summary>
        /// Deletes a local file.
        /// </summary>
        /// <param name="localFileUri">The local Uri to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static void DeleteFile(
            String localFileUri,
            IBdoLog log = null)
        {
            try
            {
                if (File.Exists(localFileUri))
                {
                    Directory.Delete(localFileUri, true);
                    log?.AddMessage("File '" + localFileUri + "' deleted");
                }
                else
                    log?.AddError("Could not delete file '" + localFileUri + "'");
            }
            catch (Exception exception)
            {
                log?.AddException(exception);
            }
        }

        /// <summary>
        /// Deletes a local folder.
        /// </summary>
        /// <param name="localfolderUri">The local Uri to consider.</param>
        /// <param name="log">The log to consider.</param>
        public static void DeleteFolder(
            String localfolderUri,
            IBdoLog log = null)
        {
            try
            {
                if (Directory.Exists(localfolderUri))
                {
                    Directory.Delete(localfolderUri, true);
                    log?.AddMessage("Folder '" + localfolderUri + "' deleted");
                }
                else
                    log?.AddError("Could not delete folder '" + localfolderUri + "'");
            }
            catch (Exception exception)
            {
                log?.AddException("Could not delete folder '" + localfolderUri + "'", description: exception.ToString());
            }
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
        public override void DeleteItems(
            String folderUri,
            String filter,
            DateTime timeLimit,
            Boolean isRecursive,
            IBdoLog log = null,
            CarrierKind_standard fileKind = CarrierKind_standard.Any)
        {
            foreach (RepositoryItem item in this.GetFiles(
                folderUri, filter, isRecursive, log, fileKind))
            {
                if (item.LastWriteDate != null)
                {
                    DateTime lastWriteDateTime;
                    if ((timeLimit == null) ||
                        ((DateTime.TryParse(item.LastWriteDate, out lastWriteDateTime)) &&
                        (DateTime.Now.Subtract(lastWriteDateTime).Ticks > timeLimit.Ticks)))
                        if (item is RepositoryFolder)
                            BdoNFSConnection.DeleteFolder(item.Path, log);
                        else if (item is RepositoryFile)
                            BdoNFSConnection.DeleteFile(item.Path, log);
                }
            }
        }

        #endregion
    }
}
