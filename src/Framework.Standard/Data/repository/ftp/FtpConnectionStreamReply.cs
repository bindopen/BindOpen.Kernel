using System;
using System.Security.Authentication;

namespace dkm.standard.data.repository.ftp
{

    /// <summary>
    /// This class represents a FTP connection stream reply.
    /// </summary>
    public class FtpConnectionStreamReply
    {

        // ---------------------------------
        // SUB CLASSES
        // ---------------------------------

        #region Sub Classes

        /// <summary>
        /// This classes represents the information about a SSL/TLS connection.
        /// </summary>
        public class SslInformation
        {

            // ---------------------------------
            // PROPERTIES
            // ---------------------------------

            #region Properties

            /// <summary>
            /// SSL protocol of this instance.
            /// </summary>
            public SslProtocols SslProtocol
            {
                get;
                set;
            }

            /// <summary>
            /// Cipher algorithm of this instance.
            /// </summary>
            public CipherAlgorithmType CipherAlgorithm
            {
                get;
                set;
            }

            /// <summary>
            /// Cipher strength of this instance.
            /// </summary>
            public int CipherStrength
            {
                get;
                set;
            }

            /// <summary>
            /// Hash algorithm type of this instance.
            /// </summary>
            public HashAlgorithmType HashAlgorithm
            {
                get;
                set;
            }

            /// <summary>
            /// Hash strength of this instance.
            /// </summary>
            public int HashStrength
            {
                get;
                set;
            }

            /// <summary>
            /// Hash exchange algorithm type of this instance.
            /// </summary>
            public ExchangeAlgorithmType KeyExchangeAlgorithm
            {
                get;
                set;
            }

            /// <summary>
            /// Key exchange strength of this instance.
            /// </summary>
            public int KeyExchangeStrength
            {
                get;
                set;
            }

            #endregion


            // ---------------------------------
            // ACCESSORS
            // ---------------------------------

            #region Accessors

            /// <summary>
            /// Gets the string representing this instance.
            /// </summary>
            /// <returns>Returns the string representing this instance.</returns>
            public override string ToString()
            {
                return this.SslProtocol.ToString() + ", " +
                       this.CipherAlgorithm.ToString() + " (" + this.CipherStrength.ToString() + " bits), " +
                       this.KeyExchangeAlgorithm.ToString() + " (" + this.KeyExchangeStrength.ToString() + " bits), " +
                       this.HashAlgorithm.ToString() + " (" + this.HashStrength.ToString() + " bits)";
            }

        #endregion
        }

        #endregion


        // ---------------------------------
        // PROPERTIES
        // ---------------------------------

        #region Properties

        /// <summary>
        /// The code of this instance.
        /// </summary>
        public int Code
        {
            get;
            set;
        }

        /// <summary>
        /// The message of this instance.
        /// </summary>
        public String Message
        {
            get;
            set;
        }

        #endregion

        
        // ---------------------------------
        // ACCESSORS
        // ---------------------------------

        #region Accessors

        /// <summary>
        /// Gets the string representing this instance.
        /// </summary>
        /// <returns>Returns the string representing this instance.</returns>
        public override String ToString()
        {
            return string.Format("[{0}] {1}", this.Code, this.Message);
        }

        #endregion

    }


}
