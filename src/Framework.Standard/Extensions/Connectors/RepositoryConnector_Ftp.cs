using cor_runtime_wdl.application.scope;
using dkm.core.system.diagnostics;
using System;

namespace dkm.standard.extension.connectors.ftp
{

    /// <summary>
    /// This class represents a FTP file connector.
    /// </summary>
   public class RepositoryConnector_Ftp : RepositoryConnector
   {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_Ftp class.
        /// </summary>
        public RepositoryConnector_Ftp()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryConnector_Ftp class.
        /// </summary>
        /// <param name="aName">The name to consider.</param>
        /// <param name="aDataSource">The OleDb database data source to consider.</param>
        /// <param name="aAppScope">The application scope to consider.</param>
        /// <param name="aLog">The log of creation.</param>
        public RepositoryConnector_Ftp(
            String aName,
            DataSource_Ftp aDataSource,
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
        public new DataSource_Ftp GetDataSource()
        {
            return this.DataSource as DataSource_Ftp;
        }

        #endregion


        // ---------------------------------
        // FILE MANAGEMENT
        // ---------------------------------

        #region File Management

        //// Pull ---------------------------------------

        ///// <summary>
        ///// Gets a remote file to a local URI.
        ///// </summary>
        ///// <param name="aRemoteFileURI">The remote URI to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        //public override void Get(String aRemoteFileURI, String aLocalPathURI)
        //{
        //    if (this.myClient != null)
        //        this.myClient.GetFile(aRemoteFileURI, aLocalPathURI);
        //}

        ///// <summary>
        ///// Gets remote files in folder to a local URI.
        ///// </summary>
        ///// <param name="aRemoteFileURI">The remote URI to consider.</param>
        ///// <param name="aRemoteFilter">The remote filter to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        //public override void Get(string aRemoteFileURI, string aRemoteFilter, string aLocalPathURI)
        //{
        //    if (this.myClient != null)
        //        this.myClient.GetFiles(aRemoteFileURI, aLocalPathURI, aRemoteFilter, FtpConnectionClient.SchemaStyles.Wildcard, true, null);
        //}

        ///// <summary>
        ///// Gets remote files to a local URI.
        ///// </summary>
        ///// <param name="someRemoteFileURIs">The remote URIs to consider.</param>
        ///// <param name="aLocalPathURI">The URI of the local path to consider.</param>
        //public override void Get(List<String> someRemoteFileURIs, String aLocalPathURI)
        //{
        //    foreach (String aRemoteFileURI in someRemoteFileURIs)
        //        this.Get(aRemoteFileURI, aLocalPathURI);
        //}

        //// Push -----------------------------------------------

        ///// <summary>
        ///// Posts a local file to a remote URI.
        ///// </summary>
        ///// <param name="aLocalFileUri">The URI of the local file.</param>
        ///// <param name="aRemotePathUri">The URI of the remote path.</param>
        //public override void Post(String aLocalFileUri, String aRemotePathUri)
        //{
        //    if (this.myClient != null)
        //        this.myClient.PutFile(aLocalFileUri, aRemotePathUri);
        //}

        ///// <summary>
        ///// Posts local files in folder to a remote URI.
        ///// </summary>
        ///// <param name="aLocalFileURI">The local URI to consider.</param>
        ///// <param name="aLocalFilter">The local filter to consider.</param>
        ///// <param name="aRemotePathURI">The URI of the remote path to consider.</param>
        //public override void Post(string aLocalFileURI, string aLocalFilter, string aRemotePathURI)
        //{
        //    if (this.myClient != null)
        //        this.myClient.PutFiles(aLocalFileURI, aRemotePathURI, aLocalFilter, FtpConnectionClient.SchemaStyles.Wildcard, true, null);
        //}

        ///// <summary>
        ///// Posts local files to a remote URI.
        ///// </summary>
        ///// <param name="someLocalFileURIs">The local URIs to consider.</param>
        ///// <param name="aRemotePathURI">The URI of the remote path to consider.</param>
        //public override void Post(List<String> someLocalFileURIs, String aRemotePathURI)
        //{
        //    foreach (String aRemoteFileURI in someLocalFileURIs)
        //        this.Post(aRemoteFileURI, aRemotePathURI);
        //}

        //// Open / Close ---------------------------------------

        ///// <summary>
        ///// Opens a connection.
        ///// </summary>
        //public override Log Open()
        //{
        //    base.Open();
        //    if (this.Configuration != null)
        //        if (!String.IsNullOrEmpty(this.Configuration.Host))
        //        {
        //            this.myClient = new FtpConnectionClient();
        //            this.myClient.Connect(
        //                this.Configuration.Host,
        //                 new NetworkCredential(
        //                     this.Configuration.Login,
        //                     this.Configuration.Password),
        //                     FtpConnectionClient.ESSLSupportModes.DataChannelRequested);
        //        }
        //}

        ///// <summary>
        ///// Closes the existing connection.
        ///// </summary>
        //public override Log Close()
        //{
        //    if (this.myClient != null)
        //        this.myClient.Close();
        //}

        //// Browser ---------------------------------------

        ///// <summary>
        ///// Gets the list of elements of the remote folder.
        ///// </summary>
        ///// <param name="aRemoteFolderUri">The URI of the remote folder path to consider.</param>
        ///// <param name="aFileKind">The kind of elements to consider.</param>
        ///// <returns>Lists of elements of the remote folder.</returns>
        //public override List<File> GetFolderList(
        //    String aRemoteFolderUri,
        //    File.FileKind aFileKind = File.FileKind.Any)
        //{
        //    List<File> someRepositoryItemInformations = new List<File>();

        //    if (aRemoteFolderUri == null) return someRepositoryItemInformations;
        //    if (this.myClient == null) return someRepositoryItemInformations;

        //    if (!aRemoteFolderUri.EndsWith("/"))
        //        aRemoteFolderUri += "/";

        //    if (aRemoteFolderUri != "/")
        //        someRepositoryItemInformations.Add(new File
        //        {
        //            Kind = File.FileKind.Folder,
        //            FullPath = (aRemoteFolderUri.Length <= 1 ? aRemoteFolderUri :
        //                 (aRemoteFolderUri.Contains("/") ? aRemoteFolderUri.Substring(0, aRemoteFolderUri.LastIndexOf("/", aRemoteFolderUri.Length - 2)) + "/" : aRemoteFolderUri)),
        //            ParentPath = "",
        //            Name = ".."
        //        });

        //    foreach (File aFile in this.myClient.GetDirectoryList(aRemoteFolderUri))
        //    {
        //        aFile.FullPath = aRemoteFolderUri + aFile.Name;
        //        aFile.ParentPath = aRemoteFolderUri;
        //        someRepositoryItemInformations.Add(aFile);
        //    }

        //    // we return the orederd list

        //    return someRepositoryItemInformations.OrderBy(p=> p.Kind!= File.FileKind.Folder).ToList();
        //}

        #endregion

    }
}
