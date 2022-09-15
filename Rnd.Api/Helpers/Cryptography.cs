using System.Security.Cryptography;

namespace Rnd.Api.Helpers;

public static class Cryptography
    {
        /// <summary>
        /// Encrypts a text to ciphertext using salt to generate a key
        /// </summary>
        public static async Task<byte[]> EncryptAsync(string? text, string salt)
        {
            if (String.IsNullOrEmpty(text))
            {
                return Array.Empty<byte>();
            }

            if (String.IsNullOrEmpty(salt))
            {
                throw new Exception("Encryption required, but salt is not specified in config");
            }

            // Create a new instance of the Aes class. This generates a new initialization vector (IV).
            using var aes = Aes.Create();

            // Encrypt the text to an array of bytes. Using key created from salt.
            var encrypted = await EncryptStringToBytesAsync(text, CreateKey16Bytes(salt), aes.IV);

            // merge the iv to start of cipher byte array 
            var result = aes.IV.Concat(encrypted).ToArray();
            return result;
        }

        /// <summary>
        /// Decrypts a byte array using salt to generate a key
        /// </summary>
        public static async Task<string> DecryptAsync(byte[]? textEncrypted, string salt)
        {
            if (textEncrypted == null || textEncrypted.Length == 0)
            {
                return String.Empty;
            }

            if (String.IsNullOrEmpty(salt))
            {
                throw new Exception("There are encrypted text, but salt is not specified in config");
            }

            const int ivLength = 16;
            // Splitting a byte array into an iv and a cipher
            var iv = new byte[ivLength];
            Array.Copy(textEncrypted, iv, ivLength);
            var cipher = new byte[textEncrypted.Length - ivLength];
            Array.Copy(textEncrypted, ivLength, cipher, 0, textEncrypted.Length - ivLength);

            var decrypted = await DecryptStringFromBytesAsync(cipher, CreateKey16Bytes(salt), iv);
            return decrypted;
        }

        /// <summary>
        /// Creates a 16-byte array from string by md5 hash func
        /// </summary>
        private static byte[] CreateKey16Bytes(string input)
        {
            return Hash.GenerateBinaryHash(input);
        }

        /// <summary>
        /// Encrypts any string into a byte array cipher
        /// </summary>
        /// <param name="text">Input string</param>
        /// <param name="key">16,20,24,28,32 byte array key</param>
        /// <param name="iv">16 byte array initialization vector</param>
        /// <returns>byte array cipher</returns>
        private static async Task<byte[]> EncryptStringToBytesAsync(string text, byte[] key, byte[] iv)
        {
            // Check arguments
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            CheckAesArguments(key, iv);

            // Create an Aes object with the specified key and IV
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            // Create an encryptor to perform the stream transform
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Create the streams used for encryption
            await using var memoryStream = new MemoryStream();
            await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            await using (var writer = new StreamWriter(cryptoStream))
            {
                // Write all data to the stream.
                await writer.WriteAsync(text).ConfigureAwait(false);
            }

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Decrypts a byte array cipher into a string
        /// </summary>
        /// <param name="cipher">byte array cipher</param>
        /// <param name="key">16,20,24,28,32 byte array key</param>
        /// <param name="iv">16 byte array initialization vector</param>
        /// <returns>Decrypted text string</returns>
        private static async Task<string> DecryptStringFromBytesAsync(byte[] cipher, byte[] key, byte[] iv)
        {
            // Check arguments
            if (cipher is not {Length: > 0})
            {
                throw new ArgumentNullException(nameof(cipher));
            }

            CheckAesArguments(key, iv);

            // Create an Aes object with the specified key and IV.
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            // Create a decryptor to perform the stream transform.
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Create the streams used for decryption.
            await using var memoryStream = new MemoryStream(cipher);
            await using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);

            // Read the decrypted bytes from the decrypting stream and place them in a string.
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks if arguments are suitable for use in the aes algorithm, throws an exception if they ain't
        /// </summary>
        /// <param name="key">16,20,24,28,32 byte array key</param>
        /// <param name="iv">16 byte array initialization vector</param>
        private static void CheckAesArguments(byte[] key, byte[] iv)
        {
            if (key is not {Length: > 0})
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!new[] {16, 20, 24, 28, 32}.Contains(key.Length))
            {
                throw new ArgumentException("Wrong length of key");
            }

            if (iv is not {Length: > 0})
            {
                throw new ArgumentNullException(nameof(iv));
            }

            if (iv.Length != 16)
            {
                throw new ArgumentException("Wrong length of initialization vector");
            }
        }
    }