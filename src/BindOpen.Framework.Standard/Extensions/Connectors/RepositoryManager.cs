using cor_runtime_wdl.application.scope;
using dkm.core.extensions.items.connectors;
using dkm.core.system.diagnostics;
using dkm.standard.extension.carriers.file;
using System;
using System.Collections.Generic;

namespace dkm.standard.data
{

    /// <summary>
    /// This class represents a repository manager.
    /// </summary>
    public class RepositoryManager
    {

        // ---------------------------------
        // VARIABLES
        // ---------------------------------

        #region Variables

        private DataConnector myDataConnector = null;
        private AppScope myAppScope = null;

        #endregion


        // ---------------------------------
        // CONSTRUCTORS
        // ---------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryManager class.
        /// </summary>
        /// <param name="aDataConnector">The data connector to consider.</param>
        /// <param name="aAppScope">The application scope to consider.</param>
        public RepositoryManager(DataConnector aDataConnector, AppScope aAppScope)
            : base()
        {
            this.myDataConnector = aDataConnector;
            this.myAppScope = aAppScope;
        }

        #endregion


        // ---------------------------------
        // FILE MANAGEMENT
        // ---------------------------------

        #region File_Management

        // Open / Close ---------------------------------------

        /// <summary>
        /// Opens a connection.
        /// </summary>
        public override Log Open()
        {
            return (this.myDataConnector==null ? null : this.myDataConnector.Open());
        }

        /// <summary>
        /// Closes the existing connection.
        /// </summary>
        public override Log Close()
        {
            return (this.myDataConnector == null ? null : this.myDataConnector.Close());
        }

        /// <summary>
        /// Indicates whether the instance is connected.
        /// </summary>
        public override Boolean IsConnected()
        {
            return (this.myDataConnector == null ? false : this.myDataConnector.IsConnected());
        }

        // Get ----------------------------------------

        /// <summary>
        /// Gets the files in the specified path.
        /// </summary>
        /// <param name="aRepositoryFile">The repository file to consider.</param>
        /// <returns>The repository files represented by the specified repository file.</returns>
        public static List<File> GetRepositoryFiles(
            File aRepositoryFile)
        {
            if (aRepositoryFile == null)
                return null;

            List<File> someRepositoryFiles = new List<File>();

            foreach (String aFilePath in RepositoryManager.GetFilePaths(aRepositoryFile))
                someRepositoryFiles.Add(new RepositoryFile(aFilePath)
                {
                    //Configuration = aRepositoryFile.Configuration,
                    PropertyStatement = aRepositoryFile.PropertyStatement
                });

            return someRepositoryFiles;
        }

        //// Pull ---------------------------------------

        ///// <summary>
        ///// Pulls a remote file to a local URI.
        ///// </summary>
        ///// <param name="aRemoteFileURI">The remote URI to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the local file can be overwritten.</param>
        //public virtual Log Pull(
        //    String aRemoteFileURI,
        //    String aLocalPathURI,
        //    Boolean aCanOverwrite)
        //{
        //    return new Log();
        //}

        ///// <summary>
        ///// Pulls a remote files in folder to a local URI.
        ///// </summary>
        ///// <param name="aRemoteFileURI">The remote URI to consider.</param>
        ///// <param name="aRemoteFilter">The remote filter to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the local files can be overwritten.</param>
        ///// <param name="aIsRecursive">Indicates whether the search is folder recursive.</param>
        //public virtual Log Pull(
        //    String aRemoteFileURI,
        //    String aRemoteFilter,
        //    String aLocalPathURI,
        //    Boolean aCanOverwrite,
        //    Boolean aIsRecursive = false)
        //{
        //    Log aLog = new Log();

        //    List<File> someFiles = this.GetFiles(
        //        aRemoteFileURI,
        //        aRemoteFilter,
        //        aIsRecursive,
        //        File.FileKind.File);
        //    foreach (File aFile in someFiles)
        //        aLog.AddEvents(this.Pull(aFile.FullName, aLocalPathURI, aCanOverwrite));
        //    return aLog;
        //}

        ///// <summary>
        ///// Pulls remote files to a local URI.
        ///// </summary>
        ///// <param name="someRemoteFileURIs">The remote URIs to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the local files can be overwritten.</param>
        //public virtual Log Pull(
        //    List<String> someRemoteFileURIs,
        //    String aLocalPathURI,
        //    Boolean aCanOverwrite)
        //{
        //    Log aLog = new Log();
        //    foreach (String aRemoteFileURI in someRemoteFileURIs)
        //        aLog.AddEvents(this.Pull(aRemoteFileURI, aLocalPathURI, aCanOverwrite));

        //    return aLog;
        //}

        //// Push ---------------------------------------

        ///// <summary>
        ///// Pushes a local file to a remote URI.
        ///// </summary>
        ///// <param name="aLocalFileURI">The local URI to consider.</param>
        ///// <param name="aRemotePathURI">The URI of the remote path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the remote file can be overwritten.</param>
        //public virtual Log Push(
        //    String aLocalFileURI,
        //    String aRemotePathURI,
        //    Boolean aCanOverwrite)
        //{
        //    return new Log();
        //}

        ///// <summary>
        ///// Pushes a local file in folder to a remote URI.
        ///// </summary>
        ///// <param name="aLocalFileURI">The local URI to consider.</param>
        ///// <param name="aLocalFilter">The local filter to consider.</param>
        ///// <param name="aRemotePathURI">The URI of the remote path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the remote file can be overwritten.</param>
        ///// <param name="aIsRecursive">Indicates whether the search is folder recursive.</param>
        //public virtual Log Push(
        //    String aLocalFileURI,
        //    String aLocalFilter,
        //    String aRemotePathURI,
        //    Boolean aCanOverwrite,
        //    Boolean aIsRecursive = false)
        //{
        //    Log aLog = new Log();

        //    List<File> someFiles = this.GetFiles(
        //        aLocalFileURI,
        //        aLocalFilter,
        //        aIsRecursive,
        //        File.FileKind.File);
        //    foreach (File aFile in someFiles)
        //        aLog.AddEvents(this.Pull(aFile.FullName, aRemotePathURI, aCanOverwrite));
        //    return aLog;
        //}

        ///// <summary>
        ///// Pushes local files to a remote URI.
        ///// </summary>
        ///// <param name="someLocalFileURIs">The local URIs to consider.</param>
        ///// <param name="aRemotePathURI">The URI of the remote path to consider.</param>
        ///// <param name="aCanOverwrite">Indicates whether the remote files can be overwritten.</param>
        //public virtual Log Push(
        //    List<String> someLocalFileURIs,
        //    String aRemotePathURI,
        //    Boolean aCanOverwrite)
        //{
        //    Log aLog = new Log();
        //    foreach (String aRemoteFileURI in someLocalFileURIs)
        //        aLog.AddEvents(this.Pull(aRemoteFileURI, aRemotePathURI, aCanOverwrite));

        //    return aLog;
        //}


        //// Browser ---------------------------------------

        ///// <summary>
        ///// Gets the list of elements of the remote folder.
        ///// </summary>
        ///// <param name="aFolderURI">The URI of the folder path to consider.</param>
        ///// <param name="aFilter">The filter to consider.</param>
        ///// <param name="aIsRecursive">Indicates whether the search is folder recursive.</param>
        ///// <param name="aFileKind">The kind of elements to consider.</param>
        ///// <returns>Lists of elements of the remote folder.</returns>
        //public virtual List<File> GetFiles(
        //    String aFolderURI,
        //    String aFilter,
        //    Boolean aIsRecursive,
        //    File.FileKind aFileKind = File.FileKind.Any)
        //{
        //    return new List<File>();
        //}

        ///// <summary>
        ///// Get the paths of the files represented by the specified repository file.
        ///// </summary>
        ///// <param name="aRepositoryFile">The repository file to consider.</param>
        ///// <returns>The files represented by the specified repository file.</returns>
        //public static List<String> GetFilePaths(RepositoryFile aRepositoryFile)
        //{
        //    List<String> someFiles = new List<String>();

        //    if (aRepositoryFile == null)
        //        return someFiles;

        //    switch (aRepositoryFile.ReferenceKind)
        //    {
        //        case DocumentElementReferenceKind.Single:
        //            someFiles.Add(aRepositoryFile.GetPath());
        //            break;
        //        case DocumentElementReferenceKind.MultipleInContainer:
        //            // we get the files of the folder using the relevant
        //            try
        //            {
        //                String aFolderPath = aRepositoryFile.GetPath();

        //                RepositoryManager aRepositoryConnectionManager = RepositoryManager.Create(aRepositoryFile.ConnectionParameterStatement);
        //                if (aRepositoryConnectionManager != null)
        //                    foreach (File aFile in aRepositoryConnectionManager.GetFiles(
        //                             aFolderPath, aRepositoryFile.Filter, aRepositoryFile.IsRecursive))
        //                        someFiles.Add(aFile.FullName);
        //            }
        //            catch
        //            {
        //            }
        //            break;
        //        case DocumentElementReferenceKind.Multiple:
        //        case DocumentElementReferenceKind.Referenced:
        //            someFiles = aRepositoryFile.GetPaths();
        //            break;
        //    }
        //    return someFiles;
        //}

        ///// <summary>
        ///// Gets the first file represented by the specified repository file.
        ///// </summary>
        ///// <param name="aRepositoryFile">The repository file to consider.</param>
        ///// <returns>The first repository file represented by the specified repository file.</returns>
        //public static RepositoryFile GetFirstRepositoryFile(RepositoryFile aRepositoryFile)
        //{
        //    if (aRepositoryFile == null)
        //        return null;

        //    RepositoryFile aFirstRepositoryFile = null;

        //    List<String> someFilePaths = RepositoryManager.GetFilePaths(aRepositoryFile);
        //    if (someFilePaths.Count > 0)
        //        aFirstRepositoryFile = new RepositoryFile(someFilePaths[0])
        //        {
        //            //Configuration = aRepositoryFile.Configuration,
        //            PropertyStatement = aRepositoryFile.PropertyStatement
        //        };

        //    return aRepositoryFile;
        //}

        #endregion

    }
}
