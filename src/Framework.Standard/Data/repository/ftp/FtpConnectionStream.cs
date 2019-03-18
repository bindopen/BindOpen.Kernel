using System;
using System.IO;

namespace dkm.standard.data.repository.ftp
{
    /// <summary>
    /// Incapsulates a Stream used during FTP get and put commands.
    /// </summary>
    public class FtpConnectionStream : Stream
    {

        // ---------------------------------
        // ENUMERATIONS
        // ---------------------------------

        #region Enumarations

        /// <summary>
        /// This enumeration lists all the possible allowed operations.
        /// </summary>
        public enum FtpAllowedOperation 
        { 
            /// <summary>
            /// Read.
            /// </summary>
            Read, 
            /// <summary>
            /// Write.
            /// </summary>
            Write
        }

        #endregion


        // ---------------------------------
        // VARIABLES / DELEGATES
        // ---------------------------------

        #region Variables_Delegates
        
        // Delegates -------------------------------

        /// <summary>
        /// Call back method called when the FTP connection stream is alive.
        /// </summary>
        public delegate void FTPConnectionStreamCallback();

        // Variables -------------------------------

        private Stream myStream;
        private FTPConnectionStreamCallback myFTPConnectionStreamCallback;
        private FtpAllowedOperation myFtpAllowedOperation;

        #endregion


        // ---------------------------------
        // PROPERTIES
        // ---------------------------------

        #region Properties

        /// <summary>
        /// Indicates whether this instance can be read.
        /// </summary>
        public override bool CanRead
        {
            get
            { 
                return this.myStream.CanRead &&
                    (this.myFtpAllowedOperation & FtpAllowedOperation.Read) == FtpAllowedOperation.Read; 
            }
        }

        /// <summary>
        /// Indicates whether this instance can be seeked.
        /// </summary>
        public override bool CanSeek
        {
            get 
            { 
                return this.myStream.CanSeek; 
            }
        }

        /// <summary>
        /// Indicates whether this instance can be written.
        /// </summary>
        public override bool CanWrite
        {
            get 
            { 
                return this.myStream.CanWrite && 
                    (this.myFtpAllowedOperation & FtpAllowedOperation.Write) == FtpAllowedOperation.Write; 
            }
        }

        /// <summary>
        /// Length of this instance.
        /// </summary>
        public override long Length
        {
            get 
            { 
                return this.myStream.Length; 
            }
        }

        /// <summary>
        /// Position of this instance.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.myStream.Position;
            }
            set
            {
                this.myStream.Position = value;
            }
        }

        #endregion


        // ---------------------------------
        // CONSTRUCTORS
        // ---------------------------------

        #region Constructors        

        /// <summary>
        /// Instantiates a new instance of the FtpConnectionStream class.
        /// </summary>
        /// <param name="aStream">The stream to consider.</param>
        /// <param name="aFtpAllowedOperation">The FTP allowed operation to consider.</param>
        /// <param name="aFTPConnectionStreamCallback">The FTP connection stream call back to consider.</param>
        public FtpConnectionStream(
            Stream aStream,
            FtpAllowedOperation aFtpAllowedOperation,
            FTPConnectionStreamCallback aFTPConnectionStreamCallback)
        {
            this.myStream = aStream;
            this.myFtpAllowedOperation = aFtpAllowedOperation;
            this.myFTPConnectionStreamCallback = aFTPConnectionStreamCallback;
        }

        #endregion


        // ---------------------------------
        // MUTATORS
        // ---------------------------------

        #region Mutators

        /// <summary>
        /// Sets the length of the stream.
        /// </summary>
        /// <param name="value">The new length value.</param>
        public override void SetLength(long value)
        {
            this.myStream.SetLength(value);
        }

        #endregion


        // ---------------------------------
        // OPERATIONS
        // ---------------------------------

        #region Operations

        /// <summary>
        /// Reads stream.
        /// </summary>
        /// <param name="aBuffer">The buffer to consider.</param>
        /// <param name="aOffset">The offset to consider.</param>
        /// <param name="aCount">The count to consider.</param>
        /// <returns></returns>
        public override int Read(byte[] aBuffer, int aOffset, int aCount)
        {
            // if we cannot read then we throw an expcetion.
            if (!this.CanRead)
                throw new Exception("Reading forbidden.");

            return this.myStream.Read(aBuffer, aOffset, aCount);
        }

        /// <summary>
        /// Seeks the stream.
        /// </summary>
        /// <param name="aOffset">The offset to consider.</param>
        /// <param name="aSeekOrigin">The seek origin to consider.</param>
        /// <returns></returns>
        public override long Seek(long aOffset, SeekOrigin aSeekOrigin)
        {
            return this.myStream.Seek(aOffset, aSeekOrigin);
        }

        /// <summary>
        /// Flushes the stream.
        /// </summary>
        public override void Flush()
        {
            this.myStream.Flush();
        }

        /// <summary>
        /// Writes to the stream.
        /// </summary>
        /// <param name="aBuffer">The buffer to consider.</param>
        /// <param name="aOffset">The offset to consider.</param>
        /// <param name="aCount">The count to consider.</param>
        public override void Write(byte[] aBuffer, int aOffset, int aCount)
        {
            // if we cannot write then we throw an expcetion.
            if (!this.CanWrite)
                throw new Exception("Writing forbidden.");

            myStream.Write(aBuffer, aOffset, aCount);
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        public override void Close()
        {
            // we close the stream
            base.Close();

            // we throw the call 
            this.myFTPConnectionStreamCallback();
        }

        #endregion

    }
}
