using System;
using System.Security;

public abstract class ContactBaseImpl
{
    protected static void CheckOperationAllowed(ContactModel contact)
    {
        // The Owner must be valid.
        if (!DbBaseEntity.IsIdValid(contact.Owner)) { throw new Exception("Got an invalid user Id for the Owner. A valid Id is required for most CRUD operations."); }
        // The ActiveUser must be valid.
        if (!Db.ActiveUser.IsMyIdValid) { throw new Exception("Got an invalid user Id for the active user. A valid Id is required for most CRUD operations."); }
        // Only allowed to CRUD on contact records that belong to the current ActiveUser.
        if (contact.Owner != Db.ActiveUser.Id) { throw new SecurityException("Not allowed to update/insert/delete another owners contact."); }
    }
}
