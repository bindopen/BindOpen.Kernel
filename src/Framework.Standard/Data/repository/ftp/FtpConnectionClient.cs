#pragma warning disable 1591

using dkm.standard.extension.carriers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace dkm.standard.data.repository.ftp
{

    /// <summary>
    /// This class represents a FTP/FTPS connection client.
    /// </summary>
    public class FtpConnectionClient : IDisposable
    {

        // ---------------------------------
        // ENUMERATIONS
        // ---------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists all the possible transfer modes.
        /// </summary>
        public enum TransferModes
        { 
            /// <summary>
            /// ASCII.
            /// </summary>
            ASCII,
            /// <summary>
            /// Binary.
            /// </summary>
            Binary 
        }

        /// <summary>
        /// This enumeration lists all the possible text encodings.
        /// </summary>
        public enum TextEncodings 
        {
            /// <summary>
            /// ASCII.
            /// </summary>
            ASCII,
            /// <summary>
            /// UTF8.
            /// </summary>
            UTF8 
        }

        /// <summary>
        /// The SSL/TLS support requested or required for a connection.
        /// </summary>
        [Flags]
        public enum ESSLSupportModes
        {
            /// <summary>
            /// No SSL/TLS support. Used for standard FTP connections.
            /// </summary>
            ClearText = 0,

            /// <summary>
            /// Requests a SSL/TLS connection during authentication. 
            /// Authentication is performed using <see cref="ClearText"/> if SSL/TLS is not supported by the server.
            /// Reverts to <see cref="ClearText"/> after authetication if the CCC command is supported by the server.
            /// </summary>
            CredentialsRequested = 1,

            /// <summary>
            /// Requires a SSL/TLS connection during authentication. 
            /// Reverts to <see cref="ClearText"/> after authetication if the CCC command is supported by the server.
            /// </summary>
            CredentialsRequired = 2 | CredentialsRequested,

            /// <summary>
            /// Requests a SSL/TLS connection on the control channel. 
            /// </summary>
            /// <remarks>
            /// Acts like <see cref="CredentialsRequested"/> but does not revert to <see cref="ClearText"/> after authentication.
            /// </remarks>
            ControlChannelRequested = 4 | CredentialsRequested,

            /// <summary>
            /// Requires a SSL/TLS connection on the control channel. 
            /// </summary>
            /// <remarks>
            /// Acts like <see cref="CredentialsRequired"/> but does not revert to <see cref="ClearText"/> after authentication.
            /// </remarks>
            ControlChannelRequired = CredentialsRequired | ControlChannelRequested,

            /// <summary>
            /// Requests a SSL/TLS connection on the data channel, implies <see cref="CredentialsRequested"/>.
            /// Data transfers are not encrypted is not supported by the server.
            /// </summary>
            DataChannelRequested = 8 | CredentialsRequested,

            /// <summary>
            /// Requires a SSL/TLS connection on the data channel, implies <see cref="CredentialsRequired"/>.
            /// </summary>
            DataChannelRequired = 16 | DataChannelRequested | CredentialsRequired,

            /// <summary>
            /// Requests a SSL/TLS connection on both control and data channels, implies <see cref="ControlChannelRequested"/> and <see cref="DataChannelRequested"/>.
            /// Control channel commands and data transfers are not encrypted is not supported by the server.
            /// </summary>
            ControlAndDataChannelsRequested = ControlChannelRequested | DataChannelRequested,

            /// <summary>
            /// Requires a SSL/TLS connection on both control and data channels, implies <see cref="ControlChannelRequired"/> and <see cref="DataChannelRequired"/>.
            /// </summary>
            ControlAndDataChannelsRequired = ControlChannelRequired | DataChannelRequired,

            /// <summary>
            /// An alias for <see cref="ControlAndDataChannelsRequired"/>
            /// </summary>
            All = ControlAndDataChannelsRequired,

            /// <summary>
            /// Implicit SSL/TLS, not supported by RFC 4217. Both control channel and data channel are always encrypted.
            /// </summary>
            Implicit = 32 | ControlAndDataChannelsRequired
        }

        /// <summary>
        /// This enumeration lists all the transfer action status.
        /// </summary>
        public enum TransferActionStatus
        {
            /// <summary>
            /// Local directory created.
            /// </summary>
            LocalDirectoryCreated,
            /// <summary>
            /// Remote directory created.
            /// </summary>
            RemoteDirectoryCreated,
            /// <summary>
            /// File uploaded.
            /// </summary>
            FileUploaded,
            /// <summary>
            /// File uploading.
            /// </summary>
            FileUploadingStatus,
            /// <summary>
            /// File downloaded.
            /// </summary>
            FileDownloaded,
            /// <summary>
            /// File downloading status.
            /// </summary>
            FileDownloadingStatus
        }

        /// <summary>
        /// This enumeration lists all the possible file schema styles.
        /// </summary>
        public enum SchemaStyles
        {
            /// <summary>
            /// Interpreted as is.
            /// </summary>
            Verbatim,
            /// <summary>
            /// Interpreted as wildcard.
            /// </summary>
            Wildcard,
            /// <summary>
            /// Interpreted as a regular expression.
            /// </summary>
            Regex
        }

        /// <summary>
        /// This enumeration lists all the possible data connections.
        /// </summary>
        public enum DataConnectionModes
        {
            /// <summary>
            /// Active.
            /// </summary>
            Active,
            /// <summary>
            /// Passive.
            /// </summary>
            Passive
        }

        private enum ProtocolCodes { C, S, E, P }
        private enum AuthenticationMechanisms { TLS }
        private enum ReplicationTypes { A, E, I, L }

        #endregion

        /// <summary>
        /// Callback used during file transfers to notify the caller about any command progress. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        /// <param name="localObjectName"></param>
        /// <param name="remoteObjectName"></param>
        /// <param name="fileTransmittedBytes"></param>
        /// <param name="fileTransferSize"><c>null</c> if not available (e.g. the server does not support the SIZE command).</param>
        /// <param name="cancel"></param>
        public delegate void FileTransferCallback(FtpConnectionClient sender, TransferActionStatus action,
                                                  string localObjectName, string remoteObjectName,
                                                  ulong fileTransmittedBytes, ulong? fileTransferSize,
                                                  ref bool cancel);

        public delegate void LogCommandEventHandler(object sender, LogCommandEventArgs args);
        public delegate void LogServerReplyEventHandler(object sender, LogServerReplyEventArgs args);

        
        public class LogCommandEventArgs : EventArgs
        {
            public LogCommandEventArgs(string commandText)
                : base()
            {
                this.CommandText = commandText;
            }

            public string CommandText { get; private set; }
        }

        public class LogServerReplyEventArgs : EventArgs
        {
            public LogServerReplyEventArgs(FtpConnectionStreamReply serverReply)
                : base()
            {
                this.ServerReply = serverReply;
            }

            public FtpConnectionStreamReply ServerReply { get; private set; }
        }

        
        #region Private Fields

        TcpClient ctrlClient = null;
        StreamReader ctrlSr;
        StreamWriter ctrlSw;
        SslStream ctrlSslStream;

        TcpClient dataClient = null;
        SslStream dataSslStream;

        DataConnectionModes dataConnectionMode = DataConnectionModes.Passive;

        /// <summary>
        /// <c>true</c> to ignore the address returned by PASV
        /// </summary>
        bool useCtrlEndPointAddressForData = true;

        bool waitingCompletionReply = false;

        string hostname;

        const string anonUsername = "anonymous";
        const string anonPassword = "anonymous@FTPSClient.org"; // dummy password

        const string clntName = "AlexFTPS";

        const ESSLSupportModes defaultSSLSupportMode = ESSLSupportModes.CredentialsRequired | ESSLSupportModes.DataChannelRequested;

        ESSLSupportModes sslSupportRequestedMode;
        ESSLSupportModes sslSupportCurrentMode;

        X509Certificate sslServerCert;
        X509Certificate sslClientCert;

        FtpConnectionStreamReply.SslInformation sslInfo;

        /// <summary>
        /// 0 means no check
        /// </summary>
        int sslMinKeyExchangeAlgStrength = 0;
        int sslMinCipherAlgStrength = 0;
        int sslMinHashAlgStrength = 0;

        bool sslCheckCertRevocation = true;

        RemoteCertificateValidationCallback userValidateServerCertificate;

        int timeout = 120000; //ms

        IList<string> features = null;

        TransferModes transferMode = TransferModes.ASCII;
        TextEncodings textEncoding = TextEncodings.ASCII;

        string welcomeMessage = null;

        string bannerMessage = null;

        Stack<string> currDirStack = new Stack<string>();

        TcpListener activeDataConnListener;

        Thread keepAliveThread = null;
        volatile bool keepAlive = true;
        int keepAliveTimeout = 20000; // ms

#endregion

        #region Public Properties

        /// <summary>
        /// The requested SSL/TLS support level.
        /// </summary>
        public ESSLSupportModes SslSupportRequestedMode
        {
            get { return sslSupportRequestedMode; }
        }

        /// <summary>
        /// The current SSL/TLS support level.
        /// </summary>
        public ESSLSupportModes SslSupportCurrentMode
        {
            get { return sslSupportCurrentMode; }
        }

        /// <summary>
        /// The current text encoding
        /// </summary>
        public TextEncodings TextEncoding
        {
            get
            {
                return textEncoding;
            }
        }

        /// <summary>
        /// The current transfer mode
        /// </summary>
        public TransferModes TransferMode
        {
            get
            {
                return transferMode;
            }
        }

        /// <summary>
        /// The welcome message returned by the server during connection.
        /// </summary>
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
        }

        /// <summary>
        /// The banner message returned by the server during connection.
        /// </summary>
        public string BannerMessage
        {
            get { return bannerMessage; }
        }

        /// <summary>
        /// The server X.509 certificate used by the current connection. It is null if the connection is not encrypted.
        /// </summary>
        public X509Certificate RemoteCertificate
        {
            get
            {
                return ctrlSslStream != null ? ctrlSslStream.RemoteCertificate : null;
            }
        }

        /// <summary>
        /// The key exchange, hash and cipher algorithms used by the SSL/TLS connection or <c>null</c> if encryption is not used.
        /// </summary>
        public FtpConnectionStreamReply.SslInformation SslInfo
        {
            get
            {
                return sslInfo;
            }
        }

        /// <summary>
        /// The client X.509 certificate used by the current connection
        /// </summary>
        /// <value><c>null</c> if the connection is not using a client certificate</value>
        public X509Certificate LocalCertificate
        {
            get
            {
                return ctrlSslStream != null ? ctrlSslStream.LocalCertificate : null;
            }
        }

        /// <summary>
        /// Returns true if the keep alive thread has been started
        /// </summary>
        public bool KeepAliveStarted 
        { 
            get 
            { 
                return keepAliveThread != null; 
            } 
        }

        #endregion

        #region private Properties

        private bool IsControlChannelEncrypted
        {
            get
            {
                return ctrlSslStream != null;
            }
        }

        private bool IsDataChannelOpen
        {
            get
            {
                return (dataClient != null);
            }
        }

        #endregion

        #region Public Events

        public event LogCommandEventHandler LogCommand;
        public event LogServerReplyEventHandler LogServerReply; 

        #endregion

        #region Public Methods

        /// <summary>
        /// Anonymous authentication
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns>The text of the \"welcome message\" sent by the server.</returns>
        public string Connect(string hostname)
        {
            return this.Connect(hostname, defaultSSLSupportMode);
        }

        public string Connect(string hostname, ESSLSupportModes sslSupportMode)
        {
            return this.Connect(hostname, null, sslSupportMode);
        }

        public string Connect(string hostname, NetworkCredential credential)
        {
            return this.Connect(hostname, credential, defaultSSLSupportMode);
        }

        public string Connect(string hostname, NetworkCredential credential, ESSLSupportModes sslSupportMode)
        {
            return this.Connect(hostname, credential, sslSupportMode, null);
        }

        public string Connect(string hostname, NetworkCredential credential, ESSLSupportModes sslSupportMode, 
                            RemoteCertificateValidationCallback userValidateServerCertificate)
        {
            // Default implicit FTPS port is 990, default standard and explicit FTPS port is 21
            int port = (sslSupportMode & ESSLSupportModes.Implicit) == ESSLSupportModes.Implicit ? 990 : 21;
            return this.Connect(hostname, port, credential, sslSupportMode, userValidateServerCertificate, null, 0, 0, 0, null);
        }

        /// <summary>
        /// Connects to a FTP server using the provided parameters. 
        /// The default representation tipe is set to Binary.
        /// The text encoding is set to UTF8, if supported by the server via the FEAT command.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="credential"></param>
        /// <param name="sslSupportMode"></param>
        /// <param name="userValidateServerCertificate"></param>
        /// <param name="x509ClientCert"></param>
        /// <param name="sslMinKeyExchangeAlgStrength"></param>
        /// <param name="sslMinCipherAlgStrength"></param>
        /// <param name="sslMinHashAlgStrength"></param>
        /// <param name="timeout">Connection timeout in ms. <c>null</c> can be specifiad to keep the default value of 120s.</param>
        /// <returns>The text of the \"welcome message\" sent by the server.</returns>
        public string Connect(string hostname, int port, NetworkCredential credential, ESSLSupportModes sslSupportMode, 
                            RemoteCertificateValidationCallback userValidateServerCertificate, X509Certificate x509ClientCert, 
                            int sslMinKeyExchangeAlgStrength, int sslMinCipherAlgStrength, int sslMinHashAlgStrength, 
                            int? timeout)
        {
            return this.Connect(hostname, port, credential, sslSupportMode, userValidateServerCertificate, x509ClientCert,
                           sslMinKeyExchangeAlgStrength, sslMinCipherAlgStrength, sslMinHashAlgStrength, timeout, true);
        }

        /// <summary>
        /// Connects to a FTP server using the provided parameters. 
        /// The default representation tipe is set to Binary.
        /// The text encoding is set to UTF8, if supported by the server via the FEAT command.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="credential"></param>
        /// <param name="sslSupportMode"></param>
        /// <param name="userValidateServerCertificate"></param>
        /// <param name="x509ClientCert"></param>
        /// <param name="sslMinKeyExchangeAlgStrength"></param>
        /// <param name="sslMinCipherAlgStrength"></param>
        /// <param name="sslMinHashAlgStrength"></param>
        /// <param name="timeout">Connection timeout in ms. <c>null</c> can be specifiad to keep the default value of 120s.</param>
        /// <param name="useCtrlEndPointAddressForData"><c>true</c> to use the control channel remote address for data connections instead of the address returned by PASV</param>
        /// <returns>The text of the \"welcome message\" sent by the server.</returns>
        public string Connect(string hostname, int port, NetworkCredential credential, ESSLSupportModes sslSupportMode,
                            RemoteCertificateValidationCallback userValidateServerCertificate, X509Certificate x509ClientCert,
                            int sslMinKeyExchangeAlgStrength, int sslMinCipherAlgStrength, int sslMinHashAlgStrength,
                            int? timeout, bool useCtrlEndPointAddressForData)
        {
            return this.Connect(hostname, port, credential, sslSupportMode, userValidateServerCertificate, x509ClientCert,
                           sslMinKeyExchangeAlgStrength, sslMinCipherAlgStrength, sslMinHashAlgStrength, timeout, true, DataConnectionModes.Passive);
        }


        /// <summary>
        /// Connects to a FTP server using the provided parameters. 
        /// The default representation tipe is set to Binary.
        /// The text encoding is set to UTF8, if supported by the server via the FEAT command.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="credential"></param>
        /// <param name="sslSupportMode"></param>
        /// <param name="userValidateServerCertificate"></param>
        /// <param name="x509ClientCert"></param>
        /// <param name="sslMinKeyExchangeAlgStrength"></param>
        /// <param name="sslMinCipherAlgStrength"></param>
        /// <param name="sslMinHashAlgStrength"></param>
        /// <param name="timeout">Connection timeout in ms. <c>null</c> can be specifiad to keep the default value of 120s.</param>
        /// <param name="useCtrlEndPointAddressForData"><c>true</c> to use the control channel remote address for data connections instead of the address returned by PASV</param>
        /// <param name="dataConnectionMode">Active or Passive data connection mode</param>
        /// <returns>The text of the \"welcome message\" sent by the server.</returns>
        public string Connect(string hostname, int port, NetworkCredential credential, ESSLSupportModes sslSupportMode, 
                            RemoteCertificateValidationCallback userValidateServerCertificate, X509Certificate x509ClientCert, 
                            int sslMinKeyExchangeAlgStrength, int sslMinCipherAlgStrength, int sslMinHashAlgStrength, 
                            int? timeout, bool useCtrlEndPointAddressForData, DataConnectionModes dataConnectionMode)
        {
            this.Close();

            if (String.IsNullOrEmpty(hostname))
                return "";

            // Anonymous authentication
            if (credential == null)
                credential = new NetworkCredential(anonUsername, anonPassword);

            if (timeout != null)
                this.timeout = timeout.Value;

            this.sslClientCert = x509ClientCert;

            this.userValidateServerCertificate = userValidateServerCertificate;

            this.sslMinKeyExchangeAlgStrength = sslMinKeyExchangeAlgStrength;
            this.sslMinCipherAlgStrength = sslMinCipherAlgStrength;
            this.sslMinHashAlgStrength = sslMinHashAlgStrength;

            this.sslSupportRequestedMode = sslSupportMode;
            this.sslSupportCurrentMode = sslSupportMode;

            this.useCtrlEndPointAddressForData = useCtrlEndPointAddressForData;

            this.dataConnectionMode = dataConnectionMode;

            sslInfo = null;

            features = null;

            transferMode = TransferModes.ASCII;
            textEncoding = TextEncodings.ASCII;

            bannerMessage = null;
            welcomeMessage = null;            

            currDirStack.Clear();

            // Ok, member initialization is done. Start with setting up a control connection
            SetupCtrlConnection(hostname, port, Encoding.ASCII);

            // Used later for SSL/TLS auth
            this.hostname = hostname;

            // Implicit SSL/TLS
            bool isImplicitSsl = (sslSupportMode & ESSLSupportModes.Implicit) == ESSLSupportModes.Implicit;
            if (isImplicitSsl)
                SwitchCtrlToSSLMode();

            // Wait fot server message
            bannerMessage = GetReply().Message;

            // Explicit SSL/TLS
            if (!isImplicitSsl)
                SslControlChannelCheckExplicitEncryptionRequest(sslSupportMode);

            // Login. Note that a password might not be required
            // TODO: check if the welcomeMessage is returned by the USER command in case the PASS command is not required.  
            if(UserCmd(credential.UserName))
                welcomeMessage = PassCmd(credential.Password);

            GetFeaturesFromServer();

            if (IsControlChannelEncrypted)
                if(!isImplicitSsl)
                {
                    SslDataChannelCheckExplicitEncryptionRequest();

                    if ((sslSupportMode & ESSLSupportModes.ControlChannelRequested) != ESSLSupportModes.ControlChannelRequested)
                        SSlCtrlChannelCheckRevertToClearText();
                }
                else
                    SslDataChannelImplicitEncryptionRequest();

            try
            {
                // This is required by some FTP servers and must precede any OPTS command
                if (CheckFeature("CLNT"))
                    ClntCmd(clntName);

                // Set UTF8 as character encoding, but only if listed among the FEAT features
                if (CheckFeature("UTF8"))
                    SetTextEncoding(TextEncodings.UTF8);
            }
            catch (Exception)
            {
                //TODO: add warning info
            }

            // Default binary transfers
            SetTransferMode(TransferModes.Binary);

            return welcomeMessage;
        }

        private void KeepAliveThreadFunc()
        {
            while (keepAlive)
            {
                try
                {
                    NoopCmd();
                    Thread.Sleep(keepAliveTimeout);
                }
                catch
                {
                    // Ignore
                }
            }
        }

        /// <summary>
        /// Issues NOOP commands periodically in a separated thread
        /// </summary>
        public void StartKeepAlive()
        {
            CheckConnection();

            if (keepAliveThread != null)
                throw new FtpConnectionException("KeepAlive already started");

            keepAliveThread = new Thread(new ThreadStart(KeepAliveThreadFunc));
            keepAliveThread.Start();
        }

        /// <summary>
        /// Stops the keep alive thread 
        /// </summary>
        public void StopKeepAlive()
        {
            if (keepAliveThread != null)
            {
                keepAlive = false;
                // Interrupt any sleep/wait operation 
                keepAliveThread.Interrupt();
                keepAliveThread.Join();
                keepAliveThread = null;
            }
        }

        private void SslDataChannelImplicitEncryptionRequest()
        {
            try
            {
                // FileZilla server requires this
                if (CheckFeature("AUTH SSL") ||
                    CheckFeature("AUTH TLS") || 
                    (CheckFeature("PBSZ") && CheckFeature("PROT")))
                {
                    PbszCmd(0);
                    ProtCmd(ProtocolCodes.P);
                }                    
            }
            catch (Exception)
            {
                // Just ignore
            }
        }

        /// <summary>
        /// Set the representation type according to the given parameter
        /// </summary>
        /// <param name="transferMode"></param>
        public void SetTransferMode(TransferModes transferMode)
        {
            TypeCmd(transferMode == TransferModes.ASCII ? ReplicationTypes.A : ReplicationTypes.I, null);
            this.transferMode = transferMode;
        }

        /// <summary>
        /// Features returned from the FEAT command
        /// </summary>
        /// <returns>null if the FEAT command is not supported by the server</returns>
        public IList<string> GetFeatures()
        {
            return features != null ? new List<string>(features) : null;
        }

        /// <summary>
        /// Remote transfer file size returned by the SIZE command.
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <returns>The file transmission size in bytes, based on the current transfer mode or <c>null</c> if the SIZE command is not supported.</returns>
        public ulong? GetFileTransferSize(string remoteFileName)
        {
            // RFC 3659 4.3. SIZE must be included in the FEAT reply
            return CheckFeature("SIZE") ? (ulong?)SizeCmd(remoteFileName) : null;
        }

        /// <summary>
        /// Retrieves the given file from the server. A <see cref="FtpConnectionStream"/> is returned, to be read until the end of file.
        /// See the <see cref="GetFile(string, string)"/> overload to easily save the stream to a local file. 
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public FtpConnectionStream GetFile(string remoteFileName)
        {
            SetupDataConnection();
            RetrCmd(remoteFileName);
            return EndStreamCommand(FtpConnectionStream.FtpAllowedOperation.Read);
        }

        /// <summary>
        /// GetFile overload to easily transfer a file from remote to local
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <param name="localFileName"></param>
        /// <returns>Transferred bytes count.</returns>
        public ulong GetFile(string remoteFileName, string localFileName)
        {
            return GetFile(remoteFileName, localFileName, null);
        }

        /// <summary>
        /// GetFile overload to easily transfer a file from remote to local
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <param name="localFileName"></param>
        /// <param name="transferCallback"></param>
        /// <returns>Transferred bytes count.</returns>
        public ulong GetFile(string remoteFileName, string localFileName, FileTransferCallback transferCallback)
        {
            ulong totalBytes = 0;
            ulong? fileTransferSize = null;

            if (transferCallback != null)
                try
                {
                    fileTransferSize = GetFileTransferSize(remoteFileName);
                }
                catch (FtpConnectionException ex)
                {
                    // Give a more detailed description, insteand of, e.g.: "Could not get file size".
                    if (ex.ErrorCode == 550)
                        throw new FtpConnectionException("Could not get the requested remote file", ex);
                    else
                        throw ex;
                }

            using (Stream s = GetFile(remoteFileName))
            {
                using (FileStream fs = new FileStream(localFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {                    
                    byte[] buf = new byte[1024];
                    int n = 0;
                    do
                    {
                        CallTransferCallback(transferCallback, TransferActionStatus.FileDownloadingStatus, localFileName, remoteFileName, totalBytes, fileTransferSize);

                        n = s.Read(buf, 0, buf.Length);
                        if (n > 0)
                        {
                            fs.Write(buf, 0, n);
                            totalBytes += (ulong)n;
                        }
                    }
                    while (n > 0);

                    fs.Close();
                }
                s.Close();
            }

            CallTransferCallback(transferCallback, TransferActionStatus.FileDownloaded, localFileName, remoteFileName, totalBytes, fileTransferSize);

            return totalBytes;
        }

        /// <summary>
        /// Transfers all files, matching the given schema, from the given remote directory. Optionally recursive
        /// </summary>
        /// <param name="remoteDirectoryName">Absolute or relative remote path, null indicates the current directory</param>
        /// <param name="localDirectoryName">Absolute local path</param>
        /// <param name="fileSchema">May be null to transfer all files</param>
        /// <param name="schemaStyle"></param>
        /// <param name="recursive"></param>
        /// <param name="transferCallback"></param>
        /// <remarks>
        /// </remarks>
        public void GetFiles(string remoteDirectoryName, string localDirectoryName,
                             string fileSchema, SchemaStyles schemaStyle, bool recursive,
                             FileTransferCallback transferCallback)
        {
            GetFiles(remoteDirectoryName, localDirectoryName, fileSchema, schemaStyle, recursive, transferCallback, new List<string>());
        }

        private void GetFiles(string remoteDirectoryName, string localDirectoryName, string fileSchema, SchemaStyles schemaStyle, bool recursive, FileTransferCallback transferCallback, IList<string> paths)
        {
            Regex regex = null;
            if (fileSchema != null)
            {
                string fileRegexSchema = GetRegexSchema(fileSchema, schemaStyle);
                regex = new Regex(fileRegexSchema);
            }

            string currLocalDirectoryName = localDirectoryName;
            if (currLocalDirectoryName == null || currLocalDirectoryName.Length == 0)
                currLocalDirectoryName = Directory.GetCurrentDirectory();
            else if (!Directory.Exists(currLocalDirectoryName))
            {
                // Create local directory if needed
                Directory.CreateDirectory(currLocalDirectoryName);
                CallTransferCallback(transferCallback, TransferActionStatus.LocalDirectoryCreated, currLocalDirectoryName, null, 0, null);
            }

            string currRemoteDirectoryName = remoteDirectoryName;
            if (currRemoteDirectoryName == null || currRemoteDirectoryName.Length == 0)
                currRemoteDirectoryName = GetCurrentDirectory();

            IList<RepositoryFile> someRepositoryFiles = GetDirectoryList(currRemoteDirectoryName);

            // TODO: add SymLink dir recursion check
            CheckSymLinks(currRemoteDirectoryName, someRepositoryFiles);

            foreach (RepositoryFile item in someRepositoryFiles)
                if ((item.Kind== RepositoryFile.RepositoryFileKind.File) && (regex == null || regex.IsMatch(item.Name)))
                {
                    // Transfer file
                    string localFilePath = GetUniquePath(paths, Path.Combine(currLocalDirectoryName, FtpConnectionParser.GetCleanedFileName(item.Name)));
                    string remoteFilePath = CombineRemotePath(currRemoteDirectoryName, item.Name);
                    GetFile(remoteFilePath, localFilePath, transferCallback);
                }

            if (recursive)
                foreach (RepositoryFile item in someRepositoryFiles)
                    if (item.Kind== RepositoryFile.RepositoryFileKind.Folder)
                    {
                        // Recursion
                        string localNextDirPath = GetUniquePath(paths, Path.Combine(currLocalDirectoryName, FtpConnectionParser.GetCleanedFileName(item.Name)));
                        string remoteNextDirPath = CombineRemotePath(currRemoteDirectoryName, item.Name);
                        GetFiles(remoteNextDirPath, localNextDirPath, fileSchema, schemaStyle, recursive, transferCallback, paths);
                    }
        }

        private static string GetUniquePath(IList<string> paths, string localFilePath)
        {
            string checkPath = localFilePath;
            int i = 1;

            // TODO: check if FS is case sensitive (here we assume it's not)
            while (paths.Contains(checkPath.ToLower()))
                checkPath = localFilePath + "_" + i++;
            paths.Add(checkPath.ToLower());

            return checkPath;
        }

        /// <summary>
        /// Transfers all files, matching the given schema, from the current remote directory. Optionally recursive
        /// </summary>
        /// <param name="localDirectoryName"></param>
        /// <param name="fileSchema"></param>
        /// <param name="schemaStyle"></param>
        /// <param name="recursive"></param>
        public void GetFiles(string localDirectoryName, string fileSchema, SchemaStyles schemaStyle, bool recursive)
        {
            GetFiles(null, localDirectoryName, fileSchema, schemaStyle, recursive, null);
        }

        /// <summary>
        /// Transfers all files from the current remote directory. Optionally recursive
        /// </summary>
        /// <param name="localDirectoryName"></param>
        /// <param name="recursive"></param>
        public void GetFiles(string localDirectoryName, bool recursive)
        {
            GetFiles(null, localDirectoryName, null, SchemaStyles.Verbatim, recursive, null);
        }

        /// <summary>
        /// Transfers all files from the current remote directory without recursion
        /// </summary>
        /// <param name="localDirectoryName"></param>
        public void GetFiles(string localDirectoryName)
        {
            GetFiles(null, localDirectoryName, null, SchemaStyles.Verbatim, false, null);
        }

        /// <summary>
        /// Stores a remote file, returning a <see cref="FtpConnectionStream"/> to be used for writing the file contents. Call <see cref="FtpConnectionStream.Close"/> on the stream once done.
        /// See the <see cref="PutFile(string, string)"/> overload to easily transfer the contents of a local file. 
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public FtpConnectionStream PutFile(string remoteFileName)
        {
            SetupDataConnection();
            StorCmd(remoteFileName);
            return EndStreamCommand(FtpConnectionStream.FtpAllowedOperation.Write);
        }

        /// <summary>
        /// PutFile overload to easily transfer a file from local to remote
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        /// <returns>Transferred bytes count.</returns>
        public ulong PutFile(string localFileName, string remoteFileName)
        {
            return PutFile(localFileName, remoteFileName, null);
        }

        /// <summary>
        /// PutFile overload to easily transfer a file from local to remote
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        /// <param name="transferCallback"></param>
        /// <returns>Transferred bytes count.</returns>
        public ulong PutFile(string localFileName, string remoteFileName, FileTransferCallback transferCallback)
        {
            using (Stream s = PutFile(remoteFileName))
                return SendFile(localFileName, remoteFileName, s, transferCallback);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDirectoryName"></param>
        /// <param name="remoteDirectoryName"></param>
        /// <param name="fileSchema"></param>
        /// <param name="schemaStyle"></param>
        /// <param name="recursive"></param>
        /// <param name="transferCallback"></param>
        public void PutFiles(string localDirectoryName, string remoteDirectoryName,
                             string fileSchema, SchemaStyles schemaStyle, bool recursive,
                             FileTransferCallback transferCallback)
        {
            Regex regex = null;
            if (fileSchema != null)
            {
                string fileRegexSchema = GetRegexSchema(fileSchema, schemaStyle);
                regex = new Regex(fileRegexSchema);
            }

            string previuosDirName = null;
            string currRemoteDirectoryName = remoteDirectoryName;
            if (currRemoteDirectoryName != null)
            {
                previuosDirName = GetCurrentDirectory();
                EnsureDir(currRemoteDirectoryName, transferCallback);
            }
            else
                currRemoteDirectoryName = GetCurrentDirectory();

            string currLocalDirectoryName = localDirectoryName;
            if (currLocalDirectoryName == null || currLocalDirectoryName.Length == 0)
                currLocalDirectoryName = Directory.GetCurrentDirectory();

            string[] dirItems = Directory.GetFiles(currLocalDirectoryName);

            foreach (string itemName in dirItems)
            {
                String fileName = Path.GetFileName(itemName);
                if (regex == null || regex.IsMatch(fileName))
                {
                    string remoteFileName = CombineRemotePath(currRemoteDirectoryName, fileName);
                    PutFile(itemName, remoteFileName, transferCallback);
                }
            }

            if (recursive)
            {
                dirItems = Directory.GetDirectories(currLocalDirectoryName);
                foreach (string itemName in dirItems)
                {
                    string remoteNextDirName = CombineRemotePath(currRemoteDirectoryName, Path.GetFileName(itemName));
                    PutFiles(itemName, remoteNextDirName, fileSchema, schemaStyle, recursive, transferCallback);
                }
            }

            if (previuosDirName != null)
                SetCurrentDirectory(previuosDirName);
        }

        /// <summary>
        /// Transfers all files, matching the given schema, to the current remote directory. Optionally recursive
        /// </summary>
        /// <param name="localDirectoryName"></param>
        /// <param name="fileSchema"></param>
        /// <param name="schemaStyle"></param>
        /// <param name="recursive"></param>
        public void PutFiles(string localDirectoryName, string fileSchema, SchemaStyles schemaStyle, bool recursive)
        {
            PutFiles(localDirectoryName, null, fileSchema, schemaStyle, recursive, null);
        }

        /// <summary>
        /// Transfers all files to the current remote directory. Optionally recursive
        /// </summary>
        /// <param name="localDirectoryName"></param>
        /// <param name="recursive"></param>
        public void PutFiles(string localDirectoryName, bool recursive)
        {
            PutFiles(localDirectoryName, null, null, SchemaStyles.Verbatim, recursive, null);
        }

        /// <summary>
        /// Transfers all files to the current remote directory without recursion
        /// </summary>
        /// <param name="localDirectoryName"></param>
        public void PutFiles(string localDirectoryName)
        {
            PutFiles(localDirectoryName, null, null, SchemaStyles.Verbatim, false, null);
        }

        public FtpConnectionStream AppendFile(string remoteFileName)
        {
            SetupDataConnection();
            AppeCmd(remoteFileName);
            return EndStreamCommand(FtpConnectionStream.FtpAllowedOperation.Write);
        }

        /// <summary>
        /// AppendFile overload to easily transfer a file from local to remote
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        public ulong AppendFile(string localFileName, string remoteFileName)
        {
            return AppendFile(localFileName, remoteFileName, null);
        }

        /// <summary>
        /// AppendFile overload to easily transfer a file from local to remote
        /// </summary>
        public ulong AppendFile(string localFileName, string remoteFileName, FileTransferCallback transferCallback)
        {
            using (Stream s = AppendFile(remoteFileName))
                return SendFile(localFileName, remoteFileName, s, transferCallback);
        }

        public FtpConnectionStream PutUniqueFile(out string remoteFileName)
        {
            SetupDataConnection();
            StouCmd(out remoteFileName);
            return EndStreamCommand(FtpConnectionStream.FtpAllowedOperation.Write);
        }

        /// <summary>
        /// PutUniqueFile overload to easily transfer a file from local to remote
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        public ulong PutUniqueFile(string localFileName, out string remoteFileName)
        {
            return PutUniqueFile(localFileName, out remoteFileName, null);
        }

        /// <summary>
        /// PutUniqueFile overload to easily transfer a file from local to remote.
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        /// <param name="transferCallback"></param>
        public ulong PutUniqueFile(string localFileName, out string remoteFileName, FileTransferCallback transferCallback)
        {
            using (Stream s = PutUniqueFile(out remoteFileName))
                return SendFile(localFileName, remoteFileName, s, transferCallback);
        }

        /// <summary>
        /// Deletes the given remote file.
        /// </summary>
        public void DeleteFile(string remoteFileName)
        {
            DeleCmd(remoteFileName);
        }

        /// <summary>
        /// Renames the given remote file.
        /// </summary>
        public void RenameFile(string remoteFileNameFrom, string remoteFileNameTo)
        {
            RnfrCmd(remoteFileNameFrom);
            RntoCmd(remoteFileNameTo);
        }

        /// <summary>
        /// Creates the given remote directory.
        /// </summary>
        /// <param name="remoteDirName"></param>
        public void MakeDir(string remoteDirName)
        {
            MkdCmd(remoteDirName);
        }

        /// <summary>
        /// Removes the given remote directory.
        /// </summary>
        /// <param name="remoteDirName"></param>
        public void RemoveDir(string remoteDirName)
        {
            RmdCmd(remoteDirName);
        }

        /// <summary>
        /// Changes the remote directory to the parent directory.
        /// </summary>
        public void ChangeToUpperDir()
        {
            CdupCmd();
        }

        public IList<string> GetShortDirectoryList()
        {
            return GetShortDirectoryList(null);
        }

        /// <summary>
        /// Returns an array of file names and directories contained in the given directory. 
        /// Please use <see cref="GetShortDirectoryList(string)"/> or <see cref="GetDirectoryListUnparsed(string)"/> for more detailed directory information.
        /// </summary>
        /// <param name="remoteDirName"></param>
        /// <returns></returns>
        public IList<string> GetShortDirectoryList(string remoteDirName)
        {
            SetupDataConnection();
            NlstCmd(remoteDirName);
            string listData = GetDataString();
            GetReply();

            return new List<string>(listData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Returns a list of the contents form the given directory. A parsing is performed on the data returned fronm the LIST command.
        /// </summary>
        /// <param name="aRemoteFolderURI"></param>
        /// <returns></returns>
        /// <remarks>
        /// Please use <see cref="GetShortDirectoryList(string)"/> or <see cref="GetDirectoryListUnparsed(string)"/> in case of parsing errors, 
        /// as the contents returned from FTP servers may differ from the common DOS and UNIX formats adopted here.
        /// </remarks>
        public IList<RepositoryFile> GetDirectoryList(string aRemoteFolderURI = null)
        {
            string aReplyString = this.GetDirectoryListUnparsed(aRemoteFolderURI);
            return FtpConnectionParser.GetRepositoryFileList(aReplyString);
        }

        public string GetDirectoryListUnparsed()
        {
            return this.GetDirectoryListUnparsed(null);
        }

        /// <summary>
        /// returns the given directory list data as returned from the server, without parsing its contents.
        /// </summary>
        public string GetDirectoryListUnparsed(string remoteDirName)
        {
            SetupDataConnection();
            ListCmd(remoteDirName);
            string listData = this.GetDataString();
            this.GetReply();

            if (listData.Length == 0)
            {
                // On some systems (vsftpd) there is no error in case of non existent directories
                // Check the directory existence by explicitly CWD into it
                this.PushCurrentDirectory();
                this.SetCurrentDirectory(remoteDirName);
                this.PopCurrentDirectory();
            }

            return listData;
        }

        public string GetCurrentDirectory()
        {
            return PwdCmd();
        }
               
        /// <summary>
        /// Pushes the current remote directory on a stack, in order to easily restore it later calling <see cref="PopCurrentDirectory"/>.
        /// </summary>
        /// <returns>The current remote directory.</returns>
        public string PushCurrentDirectory()
        {
            string curDir = GetCurrentDirectory();
            currDirStack.Push(curDir);
            return curDir;
        }

        /// <summary>
        /// Restores the current directory. For details please see <see cref="PushCurrentDirectory"/>.   
        /// Throws an exception if the stack is empty.
        /// </summary>
        public string PopCurrentDirectory()
        {
            string dir = currDirStack.Pop();
            SetCurrentDirectory(dir);
            return dir;
        }

        /// <summary>
        /// Changes the remote current directory.
        /// </summary>
        /// <param name="remoteDirName"></param>
        public void SetCurrentDirectory(string remoteDirName)
        {
            CwdCmd(remoteDirName);
        }

        /// <summary>
        /// Returns some remote system information, as returned from the SYST command.
        /// </summary>
        /// <returns></returns>
        public string GetSystem()
        {
            return SystCmd();
        }

        /// <summary>
        /// Returns the modification time of the given remote file or <c>null</c> if the MDTM feature is not supported by the server.
        /// </summary>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public DateTime? GetFileModificationTime(string remoteFileName)
        {
            // RFC 3659 3.3. MDTM must be included in the FEAT reply
            return CheckFeature("MDTM") ? (DateTime?)MdtmCmd(remoteFileName) : null;
        }

        /// <summary>
        /// Set the language used by the server during the current connection.
        /// </summary>
        /// <param name="ietfLanguageTag">RFC 1766 language tag.</param>
        public void SetLanguage(string ietfLanguageTag)
        {
            LangCmd(ietfLanguageTag);
        }

        /// <summary>
        /// Set the given text encoding.
        /// </summary>
        /// <param name="textEncoding"></param>
        public void SetTextEncoding(TextEncodings textEncoding)
        {
            OptsCmd("UTF8 " + (textEncoding == TextEncodings.UTF8 ? "ON" : "OFF"));
            this.textEncoding = textEncoding;
        }

        /// <summary>
        /// Sends the given FTP command text to the server.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the parsed server reply.</returns>
        /// <remarks>In case of return codes >= 400 an exception is thrown.</remarks>
        public FtpConnectionStreamReply SendCustomCommand(string command)
        {
            return HandleCmd(command);
        }

        /// <summary>
        /// Closes the current connection, freeing resources. 
        /// </summary>
        public void Close()
        {
            StopKeepAlive();

            CloseDataConnection();
            CloseCtrlConnection();

            sslServerCert = null;
            sslClientCert = null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Copies the protocol information form the given stream.
        /// </summary>
        /// <param name="sslStream"></param>
        private void SetSslInfo(SslStream sslStream)
        {
            sslInfo = new FtpConnectionStreamReply.SslInformation()
            {
                SslProtocol = sslStream.SslProtocol,
                CipherAlgorithm = sslStream.CipherAlgorithm,
                CipherStrength = sslStream.CipherStrength,
                HashAlgorithm = sslStream.HashAlgorithm,
                HashStrength = sslStream.HashStrength,
                KeyExchangeAlgorithm = sslStream.KeyExchangeAlgorithm,
                KeyExchangeStrength = sslStream.KeyExchangeStrength
            };
        }

        /// <summary>
        /// Workaround needed because it is not possible to determine if a symlink is a directory or a file based on the UNIX style directory listing.
        /// </summary>
        /// <param name="remoteDirectoryName"></param>
        /// <param name="dirList"></param>
        private void CheckSymLinks(string remoteDirectoryName, IList<RepositoryFile> dirList)
        {
            string currDir = null;

            foreach (RepositoryFile item in dirList)
                if (item.IsSymbolicLink)
                    try
                    {
                        if (currDir == null)
                            currDir = GetCurrentDirectory();

                        // If we cannot set this item as the current directory then it is a file
                        String itemPath = CombineRemotePath(remoteDirectoryName, item.Name);
                        SetCurrentDirectory(itemPath);
                        item.Kind = RepositoryFile.RepositoryFileKind.Folder;

                        if (item.SymbolicLinkPath == null)
                            item.SymbolicLinkPath = this.GetCurrentDirectory();
                    }
                    catch (FtpConnectionException ex)
                    {
                        // Ok, it's (probably) a file
                        if (ex.ErrorCode == 550)
                            item.Kind = RepositoryFile.RepositoryFileKind.File;
                        else
                            throw ex;
                    }

            if (currDir != null)
                SetCurrentDirectory(currDir);
        }

        private void SSlCtrlChannelCheckRevertToClearText()
        {
            // Back to clear text mode, but only if supported by the server
            if (CheckFeature("CCC"))
                CccCmd();
            else
                sslSupportCurrentMode |= ESSLSupportModes.ControlChannelRequested;
        }

        private bool CheckFeature(string feature)
        {
            return features != null && features.Contains(feature);
        }

        private void GetFeaturesFromServer()
        {
            try
            {
                features = FeatCmd();
            }
            catch (FtpConnectionException)
            {
                // FEAT is not supported by the server
                features = null;
            }
        }

        private void SslDataChannelCheckExplicitEncryptionRequest()
        {
            if ((sslSupportCurrentMode & ESSLSupportModes.DataChannelRequested) == ESSLSupportModes.DataChannelRequested)
            {
                PbszCmd(0);

                try
                {
                    ProtCmd(ProtocolCodes.P);
                }
                catch (FtpConnectionException ex)
                {
                    // Note: MS FTP 7.0 returns 536, but RFC 2228 requires 534
                    if ((sslSupportCurrentMode & ESSLSupportModes.DataChannelRequired) == ESSLSupportModes.DataChannelRequired)
                        if (ex.ErrorCode == 534 || ex.ErrorCode == 536)
                            throw new FtpConnectionException("The server policy denies SSL/TLS", ex);
                        else
                            throw ex;

                    sslSupportCurrentMode ^= ESSLSupportModes.DataChannelRequired;

                    // Data channel transfers will be done in clear text
                    ProtCmd(ProtocolCodes.C);
                }
            }
        }

        private void SslControlChannelCheckExplicitEncryptionRequest(ESSLSupportModes sslSupportMode)
        {
            if ((sslSupportMode & ESSLSupportModes.CredentialsRequested) == ESSLSupportModes.CredentialsRequested)
                try
                {
                    AuthCmd(AuthenticationMechanisms.TLS);
                }
                catch (FtpConnectionException ex)
                {
                    if ((sslSupportMode & ESSLSupportModes.CredentialsRequired) == ESSLSupportModes.CredentialsRequired)
                        if (ex.ErrorCode == 530 || ex.ErrorCode == 534)
                            throw new FtpConnectionException("SSL/TLS connection not supported on server", ex);
                        else
                            throw ex;

                    sslSupportCurrentMode = ESSLSupportModes.ClearText;
                }
        }

        private static string GetRegexSchema(string fileSchema, SchemaStyles schemaStyle)
        {
            string fileRegexSchema = fileSchema;

            switch (schemaStyle)
            {
                case SchemaStyles.Wildcard:
                case SchemaStyles.Verbatim:
                    fileRegexSchema = "^" + Regex.Escape(fileSchema) + "$";
                    if (schemaStyle == SchemaStyles.Wildcard)
                        fileRegexSchema = fileRegexSchema.Replace(@"\*", ".*").Replace(@"\?", ".{1}");
                    break;
            }
            return fileRegexSchema;
        }

        private void CallTransferCallback(FileTransferCallback transferCallback, TransferActionStatus transferAction, 
                                          string localObjectName, string remoteObjectName,
                                          ulong fileTransmittedBytes, ulong? fileTransferSize)
        {
            if (transferCallback != null)
            {
                bool cancel = false;
                transferCallback(this, transferAction, localObjectName, remoteObjectName, fileTransmittedBytes, fileTransferSize, ref cancel);
                if (cancel)
                    throw new FtpConnectionException("Operation cancelled by the user");
            }
        }

        private ulong SendFile(string localFileName, string remoteFileName, Stream s, FileTransferCallback transferCallback)
        {
            ulong totalBytes = 0;

            ulong? fileTransferSize = null;
            if (transferCallback != null)
                fileTransferSize = (ulong)(new FileInfo(localFileName).Length);

            using (FileStream fs = System.IO.File.OpenRead(localFileName))
            {
                byte[] buf = new byte[1024];
                int n = 0;
                do
                {
                    CallTransferCallback(transferCallback, TransferActionStatus.FileUploadingStatus, localFileName, remoteFileName, totalBytes, fileTransferSize);

                    n = fs.Read(buf, 0, buf.Length);
                    if (n > 0)
                    {
                        s.Write(buf, 0, n);
                        totalBytes += (ulong)n;
                    }
                }
                while (n > 0);

                fs.Close();
            }
            s.Close();

            CallTransferCallback(transferCallback, TransferActionStatus.FileUploaded, localFileName, remoteFileName, totalBytes, fileTransferSize);

            return totalBytes;
        }

        /// <summary>
        /// Check if the given directory exists and create it if it doesn't.
        /// </summary>
        /// <param name="remoteDirectoryName"></param>
        /// <param name="transferCallback"></param>        
        private void EnsureDir(string remoteDirectoryName, FileTransferCallback transferCallback)
        {
            try
            {
                string currDir = GetCurrentDirectory();
                SetCurrentDirectory(remoteDirectoryName);
                // Ok, the directory exists, set the previous current directory
                SetCurrentDirectory(currDir);
            }
            catch (FtpConnectionException ex)
            {
                if (ex.ErrorCode == 550)
                {
                    MakeDir(remoteDirectoryName);                    
                    CallTransferCallback(transferCallback, TransferActionStatus.RemoteDirectoryCreated, null, remoteDirectoryName, 0, null);
                }
                else
                    throw ex;
            }
        }

        private FtpConnectionStream EndStreamCommand(FtpConnectionStream.FtpAllowedOperation allowedOp)
        {
            return new FtpConnectionStream(GetDataStream(), allowedOp,
                                 delegate() { 
                                              CloseDataConnection(); 
                                              if (waitingCompletionReply) 
                                                  GetReply(); 
                                            });
        }

        private Stream GetDataStream()
        {
            Stream s = null;

            if(dataConnectionMode == DataConnectionModes.Active)
                SetupActiveDataConnectionStep2();

            if ((sslSupportCurrentMode & ESSLSupportModes.DataChannelRequested) == ESSLSupportModes.DataChannelRequested)
            {
                if (dataSslStream == null)
                    dataSslStream = CreateSSlStream(dataClient.GetStream(), false);
                s = dataSslStream;
            }
            else
                s = dataClient.GetStream();

            return s;
        }

        private string GetDataString()
        {
            try
            {
                Stream s = GetDataStream();

                StringBuilder data = new StringBuilder();

                byte[] buf = new byte[1024];
                int n = 0;
                do
                {
                    n = s.Read(buf, 0, buf.Length);
                    data.Append(Encoding.UTF8.GetString(buf, 0, n));
                }
                while (n != 0);

                return data.ToString();
            }
            finally
            {
                CloseDataConnection();
            }            
        }

        private void SetupCtrlConnection(string hostname, int port, Encoding textEncoding)
        {
            CloseCtrlConnection();
            if (String.IsNullOrEmpty(hostname))
                return;

            ctrlClient = new TcpClient(hostname, port);

            Stream s = ctrlClient.GetStream();
            s.ReadTimeout = timeout;
            s.WriteTimeout = timeout;

            SetupCtrlStreamReaderAndWriter(s);            
        }

        private void SetupCtrlStreamReaderAndWriter(Stream s)
        {
            if (ctrlSw != null)
                ctrlSw.Flush();

            // SreamWriter's doc states that the default encoding is UTF8 without BOM.
            // Mono 2.0 seems to have a BOM anyway. Create it explicitly as a workaround
            Encoding encoding = new UTF8Encoding(false);

            ctrlSr = new StreamReader(s, encoding);
            ctrlSw = new StreamWriter(s, encoding);
            ctrlSw.NewLine = "\r\n";
        }

        private int SetupActiveDataConnectionStep1()
        {
            CloseDataConnection();

            IPEndPoint localAddr = new IPEndPoint(IPAddress.Any, 0);
            activeDataConnListener = new TcpListener(localAddr);
            activeDataConnListener.Start();

            return (activeDataConnListener.LocalEndpoint as IPEndPoint).Port;
        }

        private void SetupActiveDataConnectionStep2()
        {
            try
            {
                dataClient = activeDataConnListener.AcceptTcpClient();
            }
            finally
            {
                StopActiveDataConnListener();
            }
        }

        private void StopActiveDataConnListener()
        {
            activeDataConnListener.Stop();
            activeDataConnListener = null;
        }

        private void SetupPassiveDataConnection(IPEndPoint dataEndPoint)
        {
            CloseDataConnection();

            IPAddress addr;
            if (useCtrlEndPointAddressForData)
                addr = (ctrlClient.Client.RemoteEndPoint as IPEndPoint).Address;
            else
                addr = dataEndPoint.Address;

            // Enter passive mode                
            dataClient = new TcpClient(addr.ToString(), dataEndPoint.Port);

            SetDataClientTimeout();
        }

        private void SetDataClientTimeout()
        {
            Stream s = dataClient.GetStream();
            s.ReadTimeout = timeout;
            s.WriteTimeout = timeout;
        }

        private void SwitchCtrlToSSLMode()
        {
            ctrlSslStream = CreateSSlStream(ctrlClient.GetStream(), true);

            SetupCtrlStreamReaderAndWriter(ctrlSslStream);

            SetSslInfo(ctrlSslStream);
        }

        private SslStream CreateSSlStream(Stream s, bool leaveInnerStreamOpen)
        {
            SslStream sslStream = new SslStream(s, leaveInnerStreamOpen,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null //new LocalCertificateSelectionCallback(ValidateClientCertificate)
                );

            sslStream.ReadTimeout = timeout;
            sslStream.WriteTimeout = timeout;

            X509CertificateCollection clientCertColl = new X509CertificateCollection();
            if (sslClientCert != null)
                clientCertColl.Add(sslClientCert);

            //sslStream.AuthenticateAsClient(hostname);
            sslStream.AuthenticateAsClient(hostname, clientCertColl, SslProtocols.Default, sslCheckCertRevocation);

            CheckSslAlgorithmsStrength(sslStream);

            return sslStream;
        }

        private void CheckSslAlgorithmsStrength(SslStream sslStream)
        {
            // Check algorithms length
            if (sslMinKeyExchangeAlgStrength > 0 && sslStream.KeyExchangeStrength < sslMinKeyExchangeAlgStrength)
                throw new FtpConnectionException("The SSL/TSL key exchange algorithm strength does not fulfill the requirements: " + sslStream.KeyExchangeStrength.ToString());

            if (sslMinCipherAlgStrength > 0 && sslStream.CipherStrength < sslMinCipherAlgStrength)
                throw new FtpConnectionException("The SSL/TSL cipher algorithm strength does not fulfill the requirements: " + sslStream.CipherStrength.ToString());

            if (sslMinHashAlgStrength > 0 && sslStream.HashStrength < sslMinHashAlgStrength)
                throw new FtpConnectionException("The SSL/TSL hash algorithm strength does not fulfill the requirements: " + sslStream.HashStrength.ToString());

        }

        private void SwitchCtrlToClearMode()
        {
            ctrlSslStream.Close();
            ctrlSslStream = null;

            SetupCtrlStreamReaderAndWriter(ctrlClient.GetStream());
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool certOk = true;

            // Validate only the first time or if the certificate changes
            if (this.sslServerCert == null || !sslServerCert.Equals(certificate))
            {
                if (userValidateServerCertificate != null)
                    certOk = userValidateServerCertificate(this, certificate, chain, sslPolicyErrors);
                else if (sslPolicyErrors != SslPolicyErrors.None)
                    certOk = false;
               
                if(certOk)
                    this.sslServerCert = new X509Certificate(certificate.Export(X509ContentType.Cert));
            }

            return certOk;
        }

        private static string ParsePwdReply(FtpConnectionStreamReply reply)
        {
            int i = reply.Message.IndexOf('\"');
            if (i < 0)
                throw new FtpConnectionException(reply);

            int j = reply.Message.IndexOf('\"', i + 1);
            if (j < 0)
                throw new FtpConnectionException(reply);

            string dirName = reply.Message.Substring(i + 1, j - i - 1);
            return dirName;
        }

        private static IPEndPoint ParsePasvReply(FtpConnectionStreamReply reply)
        {
            IPEndPoint dataEndPoint;
            int i = reply.Message.IndexOf('(');
            if (i < 0)
                throw new FtpConnectionException(reply);

            int j = reply.Message.IndexOf(')', i + 1);
            if (j < 0)
                throw new FtpConnectionException(reply);

            string[] parts = reply.Message.Substring(i + 1, j - i - 1).Split(',');
            if (parts.Length != 6)
                throw new FtpConnectionException(reply);

            byte[] addr = new byte[4];
            for (i = 0; i < addr.Length; i++)
                addr[i] = byte.Parse(parts[i]);

            int port = byte.Parse(parts[4]) * 256 + byte.Parse(parts[5]);

            dataEndPoint = new IPEndPoint(new IPAddress(addr), port);
            return dataEndPoint;
        }

        private IPEndPoint ParseEpsvReply(FtpConnectionStreamReply reply)
        {
            string[] parts = reply.Message.Split('|');
            if(parts.Length != 5)
                throw new FtpConnectionException(reply);

            int port = int.Parse(parts[3]);
            return new IPEndPoint(((IPEndPoint)ctrlClient.Client.LocalEndPoint).Address, port);
        }

        private FtpConnectionStreamReply HandleCmd(string command)
        {
            return HandleCmd(command, true);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private FtpConnectionStreamReply HandleCmd(string command, bool waitForAnswer)
        {
            CheckConnection();
            CheckCommandInjection(command);

            // TODO: perfor proper synchronization
            //if (waitingCompletionReply)
            //    throw new FTPException("Cannot issue a new command while waiting for a previous one to complete");

            ctrlSw.WriteLine(command);
            ctrlSw.Flush();

            if (LogCommand != null)
                LogCommand(this, new LogCommandEventArgs(command));

            return waitForAnswer ? GetReply() : null;
        }

        private void CheckConnection()
        {
            if (ctrlClient == null)
                throw new FtpConnectionException("Not connected");
        }

        /// <summary>
        /// Basic injection check
        /// </summary>
        /// <param name="command"></param>
        private static void CheckCommandInjection(string command)
        {
            if (command.Contains("\r\n"))
                throw new FtpConnectionException("Newlines not allowed in command text");
        }

        /// <summary>
        /// Works like Path.Combine(...), but without replacing the "/" separator with Path.DirectorySeparatorChar
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        private static string CombineRemotePath(string path1, string path2)
        {
            return (path1.EndsWith("/") ? path1 : path1 + "/") + path2;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private FtpConnectionStreamReply GetReply()
        {
            try
            {
                FtpConnectionStreamReply reply = new FtpConnectionStreamReply();
                bool replyDone = false;

                do
                {
                    string replyLine = ctrlSr.ReadLine();

                    Match m = Regex.Match(replyLine, @"^([0-9]{3})([\s\-])(.*)$");

                    if (m.Success)
                    {
                        int code = int.Parse(m.Groups[1].Value);
                        string messageLine = m.Groups[3].Value;
                        replyDone = (m.Groups[2].Value == " ");

                        if (reply.Code == 0)
                        {
                            reply.Code = code;
                            reply.Message = messageLine;
                        }
                        else // Multiline message
                        {
                            if (reply.Code != code)
                                throw new FtpConnectionException(replyLine);

                            reply.Message += "\r\n" + messageLine;
                        }
                    }
                    else // Multiline message
                    {
                        if (reply.Code == 0)
                            throw new FtpConnectionException(replyLine);

                        reply.Message += "\r\n" + replyLine.TrimStart();
                    }
                }
                while (!replyDone);

                waitingCompletionReply = (reply.Code < 200);

                if (LogServerReply != null)
                    LogServerReply(this, new LogServerReplyEventArgs(reply));

                if (reply.Code >= 400)
                    throw new FtpConnectionException(reply);

                return reply;
            }
            catch (Exception)
            {
                waitingCompletionReply = false;
                throw;
            }
        }

        private void CloseCtrlConnection()
        {
            if (ctrlClient != null)
            {
                // Be polite
                try
                {
                    QuitCmd(false);
                }
                catch (Exception)
                {
                }

                if (ctrlSslStream != null)
                {
                    ctrlSslStream.Close();
                    ctrlSslStream = null;
                }

                ctrlSr.Close();
                ctrlSr = null;

                ctrlSw.Close();
                ctrlSw = null;

                ctrlClient.Close();
                ctrlClient = null;

                waitingCompletionReply = false;
            }
        }

        private void CloseDataConnection()
        {
            if (dataClient != null)
            {
                if (dataSslStream != null)
                {
                    dataSslStream.Close();
                    dataSslStream = null;
                }

                dataClient.Close();
                dataClient = null;
            }

            if (activeDataConnListener != null)
                StopActiveDataConnListener();
        }

        #region RFC 959

        private void StorCmd(string fileName)
        {
            HandleCmd("STOR " + fileName);
        }

        private void StouCmd(out string fileName)
        {
            FtpConnectionStreamReply reply = HandleCmd("STOU");
            fileName = ParseStouReply(reply);
        }

        private static string ParseStouReply(FtpConnectionStreamReply reply)
        {
            string fileName;
            int i = reply.Message.LastIndexOf(' ');
            if (i < 0)
                throw new FtpConnectionException(reply);
            fileName = reply.Message.Substring(i + 1, reply.Message.Length - i - 2);
            return fileName;
        }

        private void AppeCmd(string fileName)
        {
            HandleCmd("APPE " + fileName);
        }

        private void RetrCmd(string fileName)
        {
            HandleCmd("RETR " + fileName);
        }

        private void DeleCmd(string fileName)
        {
            HandleCmd("DELE " + fileName);
        }

        private void MkdCmd(string dirName)
        {
            HandleCmd("MKD " + dirName);
        }

        private void RmdCmd(string dirName)
        {
            HandleCmd("RMD " + dirName);
        }

        private void CdupCmd()
        {
            HandleCmd("CDUP");
        }

        private string SystCmd()
        {
            return HandleCmd("SYST").Message;
        }

        private void TypeCmd(ReplicationTypes repType, string param2)
        {
            HandleCmd("TYPE " + repType.ToString() + (param2 != null ? (" " + param2) : ""));
        }

        private string PwdCmd()
        {
            FtpConnectionStreamReply reply = HandleCmd("PWD");
            return ParsePwdReply(reply);
        }

        private void CwdCmd(string dirName)
        {
            HandleCmd("CWD " + dirName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><c>true</c> if the PASS command is required, false <c>otherwise</c>.</returns>
        private bool UserCmd(string userName)
        {
            return HandleCmd("USER " + userName).Code != 232;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Returns the server \"welcome message\" in case of successful login.</returns>
        private string PassCmd(string password)
        {
            return HandleCmd("PASS " + password).Message;
        }

        private void PortCmd()
        {
            int port = SetupActiveDataConnectionStep1();

            byte[] addr = (ctrlClient.Client.LocalEndPoint as IPEndPoint).Address.GetAddressBytes();
            string portStr = string.Format("{0},{1},{2},{3},{4},{5}", addr[0], addr[1], addr[2], addr[3], port / 256, port % 256);

            FtpConnectionStreamReply reply = HandleCmd("PORT " + portStr);
        }

        private void PasvCmd()
        {
            FtpConnectionStreamReply reply = HandleCmd("PASV");
            IPEndPoint dataEndPoint = ParsePasvReply(reply);

            // The caller has to close the data connection
            SetupPassiveDataConnection(dataEndPoint);
        }

        private AddressFamily GetCtrlConnAddressFamily()
        {
            return ((IPEndPoint)ctrlClient.Client.LocalEndPoint).AddressFamily;
        }


        private void SetupDataConnection()
        {
            if (dataConnectionMode == DataConnectionModes.Active)
                PortCmd();
            else
                switch(GetCtrlConnAddressFamily())
                {
                    case AddressFamily.InterNetwork:
                        PasvCmd();
                        break;
                    default:
                        EpsvCmd();
                        break;
                }
        }

        private void ListCmd(string dirName)
        {
            HandleCmd("LIST" + (dirName != null ? (" " + dirName) : ""));
        }

        private void NlstCmd(string dirName)
        {
            HandleCmd("NLST" + (dirName != null ? (" " + dirName) : ""));
        }

        private void RnfrCmd(string fileOldName)
        {
            HandleCmd("RNFR " + fileOldName);
        }

        private void RntoCmd(string fileNewName)
        {
            HandleCmd("RNTO " + fileNewName);
        }

        private void QuitCmd(bool waitForAnswer)
        {
            HandleCmd("QUIT", waitForAnswer);
        }

        private void NoopCmd()
        {
            HandleCmd("NOOP");
        }

        #endregion

        #region RFC 2228


        private void AuthCmd(AuthenticationMechanisms authMech)
        {
            HandleCmd("AUTH " + authMech.ToString());
            SwitchCtrlToSSLMode();
        }

        private void CccCmd()
        {
            HandleCmd("CCC");
            SwitchCtrlToClearMode();
        }

        private void ProtCmd(ProtocolCodes protCode)
        {
            HandleCmd("PROT " + protCode.ToString());
        }

        private void PbszCmd(uint maxSize)
        {
            HandleCmd("PBSZ " + maxSize.ToString());
        }

        #endregion

        #region RFC 2389

        private IList<string> FeatCmd()
        {
            FtpConnectionStreamReply reply = HandleCmd("FEAT");
            IList<string> features = new List<string>(reply.Message.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            features.RemoveAt(0);
            features.RemoveAt(features.Count - 1);

            return features;
        }

        private void OptsCmd(string command)
        {
            HandleCmd("OPTS " + command);
        }

        #endregion

        #region RFC 2428

        private void EpsvCmd()
        {
            FtpConnectionStreamReply reply = HandleCmd("EPSV");
            IPEndPoint dataEndPoint = ParseEpsvReply(reply);

            // The caller has to close the data connection
            SetupPassiveDataConnection(dataEndPoint);
        }
        #endregion

        #region RFC 2640

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ietfLanguageTag">RFC 1766 language tag</param>
        private void LangCmd(string ietfLanguageTag)
        {
            HandleCmd("LANG" + (ietfLanguageTag != null ? (" " + ietfLanguageTag) : ""));
        }

        #endregion

        #region RFC 3659

        private DateTime MdtmCmd(string fileName)
        {
            FtpConnectionStreamReply reply = HandleCmd("MDTM " + fileName);
            return ParseFTPDateTime(reply.Message);
        }

        private ulong SizeCmd(string fileName)
        {
            FtpConnectionStreamReply reply = HandleCmd("SIZE " + fileName);
            return ulong.Parse(reply.Message);
        }

        private static DateTime ParseFTPDateTime(string message)
        {
            return DateTime.ParseExact(message, "yyyyMMddHHmmss.FFF", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.AssumeUniversal);
        }

        #endregion

        #region Other FTP Commands

        private void ClntCmd(string name)
        {
            HandleCmd("CLNT " + name);
        }

        #endregion

        #endregion
    }
}

#pragma warning restore 1591