using Newtonsoft.Json;

public class UserModel : DbBaseEntity, ICanJson
{
    public string Email;

    /// <summary>
    /// TODO: this should obviously be encrypted, perhaps not even stored. If, I got time for this that is.
    /// </summary>
    public string Password;

    public UserModel(string email, string password, bool passwordNeedsHashing)
    {
        this.Email = email;
        this.Password = passwordNeedsHashing ? EncryptionController.ConvertToMd5(password) : password;
    }

    public static UserModel CreateEmpty()
    {
        return new UserModel(string.Empty, string.Empty, false);
    }

    /// <summary>
    /// Intended for faster debugging.
    /// </summary>
    public override string ToString()
    {
        return Email ?? Constants.Data.Null;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static UserModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<UserModel>(json);
    }
}
