using System.Collections.Generic;
using System.Linq;
using static ContactModel;

public class ContactController : Singleton<ContactController>
{
    #region Events
    public delegate void OnContactsModifiedHandler(object sender, List<ContactModel> newContacts, List<ContactModel> newFilteredContacts);
    public event OnContactsModifiedHandler OnContactsModified;
    private void RaiseOnContactsModified() { OnContactsModified?.Invoke(this, AllContacts, AllContactsFiltered); }
    #endregion

    #region Properties
    /// <summary>
    /// The list of all contacts. There might be more contacts if they are in the back-end and not yet synchronized.
    /// </summary>
    public List<ContactModel> AllContacts { get; private set; } = new List<ContactModel>();

    /// <summary>
    /// The list of filtered contacts from. The original list where the filter was applied onto is always ContactController.AllContacts.
    /// </summary>
    public List<ContactModel> AllContactsFiltered { get; private set; } = new List<ContactModel>();

    private ESortType LastUsedSortType = ESortType.ByName;
    private string LastUsedFilter = string.Empty;
    #endregion


    /// <summary>
    /// Loads all contacts from the LOCAL database and assigns them to both contact-lists.
    /// Note that this will clear any sortings from both lists but they can be sorted by name again by using the sortByName parameter.
    /// </summary>
    /// <param name="sortByName">Using this parameter won't chance the LastUsedSortType.</param>
    public void SetAllContactsFromDb(bool sortByName)
    {
        AllContacts = Db.ContactRepo.ReadAll();
        if (sortByName)
        {
            AllContacts = SortByName(AllContacts);
        }
        AllContactsFiltered = new List<ContactModel>(AllContacts);
        RaiseOnContactsModified();
    }

    /// <summary>
    /// Loads all contacts from the LOCAL database and assigns them to both contact-lists.
    /// The AllContactsFiltered will be refiltered and both contact lists will be resorted.
    /// </summary>
    public void SetAllContactsFromDb_WithFilterSort()
    {
        AllContacts = Db.ContactRepo.ReadAll();
        AllContactsFiltered.Clear();
        ReSortAllContacts();
        ReFilterContacts();
    }

    /// <summary>
    /// Sorts the contacts based on the last used sorting type.
    /// </summary>
    public void ReSortAllContacts()
    {
        SortAllContacts(LastUsedSortType);
    }

    /// <summary>
    /// Sorts the AllContacts list based on the sortType parameter. Does not sort the AllContactsFiltered.
    /// </summary>
    public void SortAllContacts(ESortType sortType)
    {
        switch (sortType)
        {
            case ESortType.None:
                throw new System.Exception("Please assign a SortType in the Inspector.");
            case ESortType.ByName:
                SortAllContacts_ByName();
                break;
            case ESortType.ByNameDesc:
                SortAllContacts_ByNameDesc();
                break;
            case ESortType.ByCreationDate:
                SortAllContacts_ByCreationDate();
                break;
            case ESortType.ByCreationDateDesc:
                SortAllContacts_ByCreationDateDesc();
                break;
            default:
                throw new CaseStatementMissingException(sortType);
        }
    }

    private List<ContactModel> SortByName(List<ContactModel> contactsToSort)
    {
        return contactsToSort.OrderBy(c => c.LastName).ThenBy(c => c.Name).ThenBy(c => c.MiddleName).ToList();
    }

    private List<ContactModel> SortByNameDesc(List<ContactModel> contactsToSort)
    {
        return contactsToSort.OrderByDescending(c => c.LastName).ThenByDescending(c => c.Name).ThenByDescending(c => c.MiddleName).ToList();
    }

    private List<ContactModel> SortByCreationDate(List<ContactModel> contactsToSort)
    {
        return contactsToSort.OrderBy(c => c.CreationDate).ToList();
    }

    private List<ContactModel> SortByCreationDateDesc(List<ContactModel> contactsToSort)
    {
        return contactsToSort.OrderByDescending(c => c.CreationDate).ToList();
    }

    public void SortAllContacts_ByName()
    {
        AllContacts = SortByName(AllContacts);
        AllContactsFiltered = SortByName(AllContactsFiltered);
        LastUsedSortType = ESortType.ByName;
        RaiseOnContactsModified();
    }

    public void SortAllContacts_ByNameDesc()
    {
        AllContacts = SortByNameDesc(AllContacts);
        AllContactsFiltered = SortByNameDesc(AllContactsFiltered);
        LastUsedSortType = ESortType.ByNameDesc;
        RaiseOnContactsModified();
    }

    public void SortAllContacts_ByCreationDate()
    {
        AllContacts = SortByCreationDate(AllContacts);
        AllContactsFiltered = SortByCreationDate(AllContactsFiltered);
        LastUsedSortType = ESortType.ByCreationDate;
        RaiseOnContactsModified();
    }

    public void SortAllContacts_ByCreationDateDesc()
    {
        AllContacts = SortByCreationDateDesc(AllContacts);
        AllContactsFiltered = SortByCreationDateDesc(AllContactsFiltered);
        LastUsedSortType = ESortType.ByCreationDateDesc;
        RaiseOnContactsModified();
    }

    /// <summary>
    /// First looks for LastName hits. If none were found then looks for Name hits. If none were found again then looks for MiddleName hits.
    /// TODO: Filter is really basic. It doesn't even split the filter for searching for name, lastname and middlename separately.
    /// </summary>
    public void FilterContacts(string filter, bool suppressModifiedEvent = false)
    {
        LastUsedFilter = filter;

        if (filter == string.Empty)
        {
            AllContactsFiltered = new List<ContactModel>(AllContacts);
            return;
        }

        filter = filter.ToLower();
        IEnumerable<ContactModel> matches = AllContacts.Where(c => c.LastName.ToLower().Contains(filter));
        if (matches.Count() == 0)
        {
            matches = AllContacts.Where(c => c.Name.ToLower().Contains(filter));
            if (matches.Count() == 0)
            {
                matches = AllContacts.Where(c => c.MiddleName.ToLower().Contains(filter));
            }
        }

        AllContactsFiltered = matches.ToList();
        if (!suppressModifiedEvent)
        {
            RaiseOnContactsModified();
        }
    }

    public void ReFilterContacts()
    {
        FilterContacts(LastUsedFilter);
    }
}
