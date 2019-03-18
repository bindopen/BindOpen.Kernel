using System;

namespace dkm.standard.data.repository.ftp
{
    /// <summary>
    /// This class represents a FTP connection exception.
    /// </summary>
    public class FtpConnectionException : Exception
    {

        // ---------------------------------
        // PROPERTIES
        // ---------------------------------

        #region Properties

        /// <summary>
        /// Error code of this instance.
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }

        #endregion


        // ---------------------------------
        // CONSTRUCTORS
        // ---------------------------------

        #region Constructors        

        /// <summary>
        /// Instantiates a new instance of the FtpConnectionException class.
        /// </summary>
        public FtpConnectionException()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FtpConnectionException class.
        /// </summary>
        /// <param name="aMessage">The message to consider.</param>
        public FtpConnectionException(string aMessage)
            : base(aMessage)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FtpConnectionException class.
        /// </summary>
        /// <param name="aMessage">The message to consider.</param>
        /// <param name="aException">The basic exception to consider.</param>
        public FtpConnectionException(string aMessage, Exception aException)
            : base(aMessage, aException)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the FtpConnectionException class.
        /// </summary>
        /// <param name="aFtpConnectionStreamReply">The FTP connection stream reply to consider.</param>
        public FtpConnectionException(FtpConnectionStreamReply aFtpConnectionStreamReply)
            : base(aFtpConnectionStreamReply.Message)
        {
            this.ErrorCode = aFtpConnectionStreamReply.Code;
        }

        #endregion

    }


}
