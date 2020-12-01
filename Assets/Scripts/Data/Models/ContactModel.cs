using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[Serializable]
public class ContactModel : DbBaseEntity, ICanJson
{
    /// <summary>
    /// This enum is used for sorting IEnumerable<ContactModel>.
    /// </summary>
    public enum ESortType { None = 0, ByName, ByNameDesc, ByCreationDate, ByCreationDateDesc }
    
    /// <summary>
    /// Used for retrieving the fields dynamically.
    /// </summary>
    private static readonly BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public;

    #region Properties
    public string Name;
    public string MiddleName;
    public string LastName;
    public string TelephoneNr;
    public string Description;
    public string Email;
    public string TwitterHandle;
    public int Owner;

    /// <summary>
    /// The timestamp for when this contact was initially created by the user.
    /// </summary>
    public DateTime CreationDate;

    /// <summary>
    /// The timestamp for when this contact was last updated by the user.
    /// If this contact was never updated then this will be equal to the CreationDate.
    /// </summary>
    public DateTime UpdatedDate;
    #endregion

    /// <summary>
    /// Creates an empty Contact with an invalid Id.
    /// </summary>
    public ContactModel()
    {
        SetInvalidId();
    }

    public ContactModel(int id, string name, string middleName, string lastName, string telephoneNr, string description,
                        string email, string twitterHandle, int owner, DateTime creationDate, DateTime updatedDate)
    {
        this.Id = id;
        this.Name = name;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.TelephoneNr = telephoneNr;
        this.Description = description;
        this.Email = email;
        this.TwitterHandle = twitterHandle;
        this.Owner = owner;
        this.CreationDate = creationDate;
        this.UpdatedDate = updatedDate;
    }

    /// <summary>
    /// Returns a randomly generated contact. Intended for debugging & development.
    /// </summary>
    public static ContactModel CreateRandom()
    {
        string[] names = new string[] { "John", "Yamine", "Eva", "Emily", "Pikachu", "Mason" };
        string[] middleNames = new string[] { "de", "van", "von", string.Empty, string.Empty };
        string[] lastNames = new string[] { "Lange", "Campbell", "Smith", "Groen", "Oranje", "Berg", "Groot" };
        int daysInThePast = UnityEngine.Random.Range(-10, 0);
        return new ContactModel(-1, names.GetRandom(), middleNames.GetRandom(), lastNames.GetRandom(), "06-12341234", "desc", "a@a.com", "SomeTwitterHandle", Db.ActiveUser.Id, DateTime.Now.AddDays(daysInThePast), DateTime.Now);
    }

    /// <summary>
    /// Creates a List of randomly generated contacts. Intended for debugging & development.
    /// </summary>
    /// <param name="amount">The amount of contacts to generate.</param>
    public static List<ContactModel> CreateRandom(int amount)
    {
        List<ContactModel> contacts = new List<ContactModel>();
        for (int i = 0; i < amount; i++)
        {
            contacts.Add(CreateRandom());
        }
        return contacts;
    }

    /// <summary>
    /// Note that the Id field is NOT included.
    /// </summary>
    /// <returns>The list of all public fields excluding the Id.</returns>
    public List<FieldInfo> GetAllFields()
    {
        return this.GetType().GetFields(BindingFlags).ToList();
    }

    /// <summary>
    /// Returns a string in the format [Name] [middle name] [Surname].
    /// </summary>
    public string GetFullName()
    {
        return string.Format("{0} {1}", Name, MiddleName == null ? LastName : string.Format("{0} {1}", MiddleName, LastName));
    }

    /// <summary>
    /// Intended for faster debugging.
    /// </summary>
    public override string ToString()
    {
        return GetFullName();
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static ContactModel FromJson(string json)
    {
        return JsonConvert.DeserializeObject<ContactModel>(json);
    }
}
