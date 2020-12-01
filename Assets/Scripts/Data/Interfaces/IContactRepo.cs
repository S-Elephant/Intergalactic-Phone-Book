using System.Collections.Generic;

public interface IContactRepo : IRepo<ContactModel>
{
    /// <returns>The Id of the newly inserted contact</returns>
    int Create(ContactModel contact);

    /// <param name="userId">The id of the user who's contacts to retrieve.</param>
    List<ContactModel> ReadAll();

    void Delete(ContactModel contact);

    void Update(ContactModel contact);
}
