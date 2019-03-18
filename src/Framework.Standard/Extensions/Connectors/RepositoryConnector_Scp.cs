using cor_runtime_wdl.application.scope;
using dkm.core.system.diagnostics;
using System;

namespace dkm.standard.extension.connectors.scp
{
    /// <summary>
    /// This class represents a FTP repository connector.
    /// </summary>
   public class RepositoryConnector_Scp : RepositoryConnector
   {

        // ---------------------------------
        // VARIABLES
        // ---------------------------------

        #region Variables

        //private Scp myClient;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_Scp class.
        /// </summary>
        public RepositoryConnector_Scp()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_Scp class.
        /// </summary>
        /// <param name="aName">The name to consider.</param>
        /// <param name="aDataSource">The OleDb database data source to consider.</param>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aLog">The log of creation.</param>
        public RepositoryConnector_Scp(
            String aName,
            DataSource_Scp aDataSource,
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
        public new DataSource_Scp GetDataSource()
        {
            return this.DataSource as DataSource_Scp;
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

        //public override void Get(string aRemoteFileUri, string aLocalPathUri, dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind repositoryKind)
        //{
        //    base.Get(aRemoteFileUri, aLocalPathUri, repositoryKind);
        //    switch (repositoryKind)
        //    {
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.Multiple:
        //            break;
        //        case dkm.standard.extension.repository.RepositoryFile.RepositoryFileKind.MultipleInContainer:


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
        //}


        //public override Log Open()
        //{
        //    base.Open();
        //    this.myClient = new Scp(this.Configuration.RemoteServerURI, this.Configuration.Login, this.Configuration.PassWord);
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
