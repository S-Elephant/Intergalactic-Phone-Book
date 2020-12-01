using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

/// <summary>
/// This class should only be used for debugging.
/// </summary>
public class ContactImp_HC : ContactBaseImpl, IContactRepo
{
    public int UserId { get; set; } = -1;

    public int Create(ContactModel contact)
    {
        CheckOperationAllowed(contact);
        // Create nothing because this implementation has no persistent data storage.
        return DbBaseEntity.InvalidId;
    }

    public void Delete(ContactModel contact)
    {
        CheckOperationAllowed(contact);
        // Do nothing.
    }

    public List<ContactModel> ReadAll()
    {
        if (!DbBaseEntity.IsIdValid(Db.ActiveUser.Id)) { return new List<ContactModel>(); }

        return ContactModel.CreateRandom(100);
    }

    public void Update(ContactModel contact)
    {
        CheckOperationAllowed(contact);
        // Do nothing.
    }
}
