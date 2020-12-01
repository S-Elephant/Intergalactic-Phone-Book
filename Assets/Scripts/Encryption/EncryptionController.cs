using System;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionController
{
    public static IEncryption Encryptor = new Encryption_AES();

	/// <summary>
	/// Hashes a string to MD5 (as a string).
	/// Taken from: http://wiki.unity3d.com/index.php/MD5
	/// </summary>
	public static string ConvertToMd5(string strToEncrypt)
	{
		UTF8Encoding ue = new UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// Encrypt bytes
		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
}
