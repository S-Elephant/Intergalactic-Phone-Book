using NUnit.Framework;

public class Test_Encryption
{
    private const string Originalstr = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n In viverra nec dui vel molestie. Aliquam erat volutpat. Phasellus rhoncus consequat efficitur. Vivamus in dolor volutpat, pretium enim a, ultrices ipsum. Morbi sed ligula at erat interdum venenatis ac in neque. Integer tristique, eros eget auctor posuere, orci est aliquam elit, vitae sagittis quam quam vel est. Integer eget justo diam. Nunc nec libero eu arcu consequat sodales.";

    [Test]
    public void EncryptDecryptSimpleString()
    {
        string encryptedStr = EncryptionController.Encryptor.Encrypt(Originalstr);
        string decryptedStr = EncryptionController.Encryptor.Decrypt(encryptedStr);

        Assert.AreNotEqual(Originalstr, encryptedStr);
        Assert.AreEqual(Originalstr, decryptedStr);
    }

    [Test]
    public void EncryptDecryptWithCustomPassword()
    {
        string password = "I ate a clock yesterday, it was very time-consuming.";
        string encryptedStr = EncryptionController.Encryptor.Encrypt(Originalstr, password);
        string decryptedStr = EncryptionController.Encryptor.Decrypt(encryptedStr, password);

        Assert.AreNotEqual(Originalstr, encryptedStr);
        Assert.AreEqual(Originalstr, decryptedStr);
    }

    [Test]
    public void MD5()
    {
        string hashMe = "The definition of a perfectionist: someone who wants to go from point A to point A+. —David Bez";
        string hashedStr = EncryptionController.ConvertToMd5(hashMe);
        string hashedStr2 = EncryptionController.ConvertToMd5(hashMe);


        Assert.AreNotEqual(hashMe, hashedStr);
        Assert.AreEqual(hashedStr, hashedStr2);
    }
}
