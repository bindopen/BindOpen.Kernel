using cor_runtime_wdl.application.scope;
using dkm.core.system.diagnostics;
using System;

namespace dkm.standard.extension.connectors.sftp
{
    /// <summary>
    /// This class represents a SFTP repository connector.
    /// </summary>
    public class RepositoryConnector_SFtp : RepositoryConnector
    {

        // ---------------------------------
        // VARIABLES
        // ---------------------------------

        #region Variables

        //private SFtp myClient;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_SFtp class.
        /// </summary>
        public RepositoryConnector_SFtp()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_SFtp class.
        /// </summary>
        /// <param name="aName">The name to consider.</param>
        /// <param name="aDataSource">The SFtp database data source to consider.</param>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aLog">The log of creation.</param>
        public RepositoryConnector_SFtp(
            String aName,
            DataSource_SFtp aDataSource,
            AppScope aAppScope = null,
            Log aLog = null) : base(null, aName, aDataSource, aAppScope, aLog)
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the configuration of this instance.
        /// </summary>
        public new DataSource_SFtp GetDataSource()
        {
            return this.DataSource as DataSource_SFtp;
        }

        #endregion


        // ---------------------------------
        // FILE MANAGEMENT
        // ---------------------------------

        #region File_Management


        ///// <summary>
        ///// Gets a remote file to a local URI
        ///// </summary>
        ///// <param name="aRemoteFileUri">The remote URI.</param>
        ///// <param name="aLocalUri">The URI of the local path.</param>
        //public override void Get(String aRemoteFileUri, String aLocalPathUri)
        //{
        //    this.myClient.Get(aRemoteFileUri, aLocalPathUri);
        //}

        ///// <summary>
        ///// Gets a file(s) from a remote URI
        ///// </summary>
        ///// <param name="aRemoteFileUri">The remote path</param>
        ///// <param name="aLocalPathUri">The local path</param>
        ///// <param name="repositoryKind">The Kind of the remote path</param>
        //public override void Get(string aRemoteFileUri, string aLocalPathUri, dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind repositoryKind)
        //{
        //    base.Get(aRemoteFileUri, aLocalPathUri, repositoryKind);
        //    switch (repositoryKind)
        //    {
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.Multiple:
        //            break;
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.MultipleInContainer:
        //            String[] folderFiles = (String[])this.myClient.GetFileList(aRemoteFileUri).ToArray(typeof(String));
        //            this.myClient.Get(folderFiles);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Posts a local file to a remote URI
        ///// </summary>
        ///// <param name="aLocalFileUri">The URI of the local file.</param>
        ///// <param name="aRemotePathUri">The URI of the remote path.</param>
        //public override void Post(String aLocalFileUri, String aRemotePathUri)
        //{
        //    this.myClient.Put(aLocalFileUri, aRemotePathUri);
        //}

        ///// <summary>
        ///// Post a local directory files to a remote Uri
        ///// </summary>
        ///// <param name="aLocalFileUri">The URI of the local directory</param>
        ///// <param name="aRemotePathUri">The URI of the remote</param>
        ///// <param name="repositoryKind">The Kind of the local  path (File or Directory)</param>
        //public override void Post(string aLocalFileUri, string aRemotePathUri, dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind repositoryKind)
        //{
        //    base.Post(aLocalFileUri, aRemotePathUri, repositoryKind);
        //    switch (repositoryKind)
        //    {
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.Multiple:
        //            break;
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.MultipleInContainer:
        //            FileInfo[] filesInfos = (new DirectoryInfo(aLocalFileUri)).GetFiles();
        //            String[] folderFiles = new string[filesInfos.Length];
        //            for (int i = 0; i < filesInfos.Length; i++)
        //            {
        //                folderFiles[i] = filesInfos[i].FullName;
        //            }
        //            this.myClient.Put(folderFiles);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public override Log Open()
        //{
        //    base.Open();
        //    this.myClient = new Sftp(this.Configuration.RemoteServerURI, this.Configuration.RemoteServerURI, this.Configuration.PassWord);
        //    this.myClient.Connect();
        //}

        //public override Log Close()
        //{
        //    base.Close();
        //    this.myClient.Close();
        //}

        #endregion

    }
}
