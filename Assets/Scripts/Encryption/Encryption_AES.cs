using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Encryption_AES : IEncryption
{
    //While an app specific salt is not the best practice for
    //password based encryption, it's probably safe enough as long as
    //it is truly uncommon. Also too much work to alter this answer otherwise.
    private static readonly byte[] Salt = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("D&7r}YbEzJrTTFR4Dzy>hWEXA]wZUV"));
    
    private const string DefaultPassword = ">#%&JFoW2E!LvMaVc^5jLcwyRc>fp9";

    /// <summary>
    /// Code taken from: https://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string-in-c
    /// </summary>
    /// <param name="strToEncrypt">The text to encrypt.</param>
    /// <param name="password">A password used to generate a key for decryption.</param>
    public string Encrypt(string strToEncrypt, string password)
    {
        if (string.IsNullOrEmpty(strToEncrypt)) { return strToEncrypt; }
        if (string.IsNullOrEmpty(password)) { throw new ArgumentNullException("password"); }

        string outStr = null;          // Encrypted string to return
        RijndaelManaged aesAlg = null; // RijndaelManaged object used to encrypt the data.

        try
        {
            // generate the key from the shared secret and the salt
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, Salt);

            // Create a RijndaelManaged object
            aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

            // Create a decryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                // prepend the IV
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(strToEncrypt);
                }
                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
        finally
        {
            // Clear the RijndaelManaged object.
            if (aesAlg != null)
            {
                aesAlg.Clear();
            }
        }

        // Return the encrypted bytes from the memory stream.
        return outStr;
    }

    /// <summary>
    /// Code taken from: https://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string-in-c
    /// </summary>
    /// <param name="encryptedStr">The text to decrypt.</param>
    /// <param name="password">A password used to generate a key for decryption.</param>
    public string Decrypt(string encryptedStr, string password)
    {
        if (string.IsNullOrEmpty(encryptedStr)) { return encryptedStr; }
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException("password");

        // Declare the RijndaelManaged object used to decrypt the data.
        RijndaelManaged aesAlg = null;

        // Declare the string used to hold the decrypted text.
        string plaintext = null;

        try
        {
            // generate the key from the shared secret and the salt
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, Salt);

            // Create the streams used for decryption.                
            byte[] bytes = Convert.FromBase64String(encryptedStr);
            using (MemoryStream msDecrypt = new MemoryStream(bytes))
            {
                // Create a RijndaelManaged object with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                // Get the initialization vector from the encrypted stream
                aesAlg.IV = ReadByteArray(msDecrypt);
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted bytes from the decrypting stream and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
        finally
        {
            // Clear the RijndaelManaged object.
            if (aesAlg != null)
            {
                aesAlg.Clear();
            }
        }

        return plaintext;
    }

    private static byte[] ReadByteArray(Stream s)
    {
        byte[] rawLength = new byte[sizeof(int)];
        if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
        {
            throw new SystemException("Stream did not contain properly formatted byte array");
        }

        byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
        if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
        {
            throw new SystemException("Did not read byte array properly");
        }

        return buffer;
    }

    public string Encrypt(string str)
    {
        return Encrypt(str, DefaultPassword);
    }

    public string Decrypt(string encryptedStr)
    {
        return Decrypt(encryptedStr, DefaultPassword);
    }
}
