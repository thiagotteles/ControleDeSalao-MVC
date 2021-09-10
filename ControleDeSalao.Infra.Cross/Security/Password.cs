using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeSalao.Infra.Cross.Security
{
    /// <summary>
    ///     Classe de password
    /// </summary>
    /// <remarks>
    ///     http://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string
    /// </remarks>
    public class Password
    {
        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="password">The text to encrypt.</param>
        /// <param name="login">A password used to generate a key for encryption.</param>
        public static string Cryptography(string password, string login)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(login));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToEncrypt = utf8.GetBytes(password);
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="password">The text to encrypt.</param>
        /// <param name="login">A password used to generate a key for encryption.</param>
        public static string Cryptography(string password)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes("sha1"));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToEncrypt = utf8.GetBytes(password);
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="chave">The text to decrypt.</param>
        /// <param name="login">A password used to generate a key for decryption.</param>
        public static string Decryption(string chave, string login)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(login));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToDecrypt = Convert.FromBase64String(chave);
            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return utf8.GetString(results);
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="chave">The text to decrypt.</param>
        /// <param name="login">A password used to generate a key for decryption.</param>
        public static string Decryption(string chave)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes("sha1"));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToDecrypt = Convert.FromBase64String(chave);
            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return utf8.GetString(results);
        }
    }
}
