public interface IEncryption
{
    string Encrypt(string str, string password);
    string Encrypt(string str); // Sadly constants aren't allowed in interfaces, therefore we overload.
    string Decrypt(string encryptedStr, string password);
    string Decrypt(string encryptedStr); // Sadly constants aren't allowed in interfaces, therefore we overload.

}
