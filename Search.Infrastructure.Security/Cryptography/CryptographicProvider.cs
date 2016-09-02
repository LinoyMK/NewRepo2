using Search.Infrastructure.Security.Abstract.Cryptography;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Search.Infrastructure.Security.Cryptography
{
    public class CryptographicProvider : ICryptographicProvider
    {
        /// <summary>
        /// 16 bytes
        /// </summary>
        private const string initVector = "@1B2c3D4e5F6g7H8";

        /// <summary>
        /// 
        /// </summary>
        private const int keySize = 256;

        /// <summary>
        /// 
        /// </summary>
        private const string saltValue = "FS@V2";

        /// <summary>
        /// 
        /// </summary>
        private const string hashAlgorithm = "SHA1";

        /// <summary>
        /// 
        /// </summary>
        private const int passwordIterations = 2;

        /// <summary>
        /// 
        /// </summary>
        private const int KeySize = 256;


        /// <summary>
        /// Method to encrypt string
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string passPhrase)
        {
            try
            {
                // Convert strings into byte arrays.
                // Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our plain-text into a byte array.
                // Let us assume that plain-text contains UTF8-encoded characters.
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified pass-phrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                string cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                return cipherText;
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Unable to encrypt.", ex);
            }
        }


        /// <summary>
        /// Method to decrypt string
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                // Convert strings defining encryption key characteristics into byte arrays. Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 encoding.
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our cipher-text into a byte array.
                var cipherTextBytes = Convert.FromBase64String(cipherText);

                // First, we must create a password, from which the key will be derived. This password will be generated from the specified pass-phrase and salt value. 
                // The password will be created using the specified hash algorithm. Password creation can be done in several iterations.
                var passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption key. Specify the size of the key in bytes (instead of bits).
                var keyBytes = passwordDeriveBytes.GetBytes(KeySize / 8);

                // Create uninitialized Rijndael encryption object.
                // It is reasonable to set encryption mode to Cipher Block Chaining (CBC). Use default options for other symmetric key parameters.
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

                // Generate decryptor from the existing key bytes and initialization vector. Key size will be defined based on the number of the key bytes.
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                var memoryStream = new MemoryStream(cipherTextBytes);

                // Define cryptographic stream (always use Read mode for encryption).
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data will be, allocate the buffer long enough to hold cipher-text;
                // plain-text is never longer than cipher-text.
                var plainTextBytes = new byte[cipherTextBytes.Length];

                // Start decrypting.
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plain-text string was UTF8-encoded.
                // Return decrypted string.   
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Unable to decrypt.", ex);
            }

        }
    }
}
