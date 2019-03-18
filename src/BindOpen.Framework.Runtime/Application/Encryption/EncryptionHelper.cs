using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BindOpen.Framework.Runtime.Application.Encryption
{
    /// <summary>
    /// This class represents a encryption box.
    /// </summary>
    public static class EncryptionHelper
    {

        // ---------------------------------------
        // PROCESSING
        // ---------------------------------------

        #region Processing

        /// <summary>
        /// Encrypts data.
        /// </summary>
        /// <param name="aSecretKey">The secret key to consider.</param>
        /// <param name="aSecretKeyIV">The secret IV key to consider.</param>
        /// <param name="inputBytes">The input bytes to consider.</param>
        /// <returns>Returns the decrypted data.</returns>
        public static Byte[] EncryptData(String aSecretKey, String aSecretKeyIV, Byte[] inputBytes)
        {
            Byte[] bytes = null;
            MemoryStream aMemoryStream = null;
            CryptoStream aCryptoStream = null;

            try
            {
                TripleDESCryptoServiceProvider aTripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider()
                {
                    Key = Encoding.UTF8.GetBytes(aSecretKey),
                    IV = Encoding.UTF8.GetBytes(aSecretKeyIV),
                    //Padding = PaddingMode.None
                };

                aMemoryStream = new MemoryStream();
                aCryptoStream = new CryptoStream(aMemoryStream,
                    aTripleDESCryptoServiceProvider.CreateEncryptor(),
                    CryptoStreamMode.Write);

                //byte[] aBuffer = new byte[1024];
                //int aMemoryRead = aMemoryStream.Read(aBuffer, 0, aBuffer.Length);
                //while (aMemoryRead > 0)
                //{
                //    aCryptoStream.Write(aBuffer, 0, aMemoryRead);
                //    aMemoryRead = aMemoryStream.Read(aBuffer, 0, aBuffer.Length);
                //}

                //aCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                //using (StreamWriter streamWriter = new StreamWriter(aCryptoStream))
                //{
                //    streamWriter.Write(inputBytes);
                //}
                //bytes = new Byte[aMemoryStream.Length];
                aCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                aCryptoStream.FlushFinalBlock();
                bytes = aMemoryStream.ToArray();
            }
            finally
            {
                if (aMemoryStream != null)
                    aMemoryStream.Close();
                try
                {
                    if (aCryptoStream != null)
                        aCryptoStream.Close();
                }
                catch
                { }
            }

            return bytes;
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        /// <param name="aSecretKey">The secret key to consider.</param>
        /// <param name="aSecretKeyIV">The secret IV key to consider.</param>
        /// <param name="inputBytes">The input bytes to consider.</param>
        /// <returns>Returns the decrypted data.</returns>
        public static Byte[] DecryptData(String aSecretKey, String aSecretKeyIV, Byte[] inputBytes)
        {
            Byte[] bytes = null;
            MemoryStream aMemoryStream = null;
            CryptoStream aCryptoStream = null;

            try
            {
                TripleDESCryptoServiceProvider aTripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider()
                {
                    Key = Encoding.UTF8.GetBytes(aSecretKey),
                    IV = Encoding.UTF8.GetBytes(aSecretKeyIV),
                    //Padding = PaddingMode.None
                };

                aMemoryStream = new MemoryStream(inputBytes);
                aCryptoStream = new CryptoStream(aMemoryStream,
                    aTripleDESCryptoServiceProvider.CreateDecryptor(),
                    CryptoStreamMode.Read);
                ////bytes = new Byte[aMemoryStream.Length];
                //aCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                //aCryptoStream.FlushFinalBlock();
                //bytes = aMemoryStream.ToArray();
                //using (StreamReader streamReader = new StreamReader(aCryptoStream))
                //{
                //    String data = streamReader.ReadToEnd();
                //    bytes = System.Text.Encoding.UTF8.GetBytes(data);
                //}
                bytes = new byte[inputBytes.Length];
                aCryptoStream.Read(bytes, 0, bytes.Length);
            }
            finally
            {
                if (aMemoryStream != null)
                    aMemoryStream.Close();
                try
                {
                    if (aCryptoStream != null)
                        aCryptoStream.Close();
                }
                catch
                { }
            }

            return bytes;
        }

        #endregion


    }
}
