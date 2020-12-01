using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuContactsAddEdit : MenuBase
{
    /// <summary>
    /// The ContactModel to update.
    /// </summary>
    public static ContactModel ContactToUpdate = null;

    private const string TimestampFormat = "MM/dd/yyyy hh:mm tt";

    [Header("Localization")]
    [SerializeField] private LocalizeMe TitleLocalizer = null;
    [SerializeField] private LocalizeMe SaveLocalizer = null;

    private InputField InputName = null;
    private InputField InputMiddleName = null;
    private InputField InputSurname = null;
    private InputField InputEmail = null;
    private InputField InputTelephoneNr = null;
    private InputField InputTwitter = null;
    private InputField InputDescription = null;

    [Header("Timestamp Fields")]
    public Text TxtTimestamps = null;

    [Header("Misc")]
    [SerializeField] private GameObject InputListGo = null;

    private void Start()
    {
        TxtTimestamps.text = string.Empty;
        Global.CheckNullValues(gameObject, TitleLocalizer, SaveLocalizer, TxtTimestamps, InputListGo);
        SetTitle();
        CreateInputRows();

        if (MenuMgr.Instance.MenuCrudMode == MenuMgr.ECrudMode.Update)
        {
            LoadContact();
        }
    }

    private void CreateInputRows()
    {
        // TODO: Refactor: Below should be done even more dynamically by querying the ContactModel for it's fields but I have no time for this so just hardcode it.
        InputName = CreateInputRow("Name", "EnterName");
        InputMiddleName = CreateInputRow("MiddleName", "EnterMiddleName");
        InputSurname = CreateInputRow("Surname", "EnterSurname");
        InputTelephoneNr = CreateInputRow("TelephoneNr", "EnterTelephoneNr");
        InputEmail = CreateInputRow("Email", "EnterEmail");
        InputTwitter = CreateInputRow("Twitter", "EnterTwitterHandle");
        InputDescription = CreateInputRow("Description", "EnterDescription");
    }

    private InputField CreateInputRow(string textWordId, string inputPlaceholderWordId)
    {
        GameObject nameRow = Instantiate(PrefabMgr.Instance.AddEditRow, InputListGo.transform, false);
        nameRow.GetComponent<AddEditRow>().Initialize(textWordId, inputPlaceholderWordId);
        return nameRow.GetComponentInChildren<InputField>();
    }

    private void SetTitle()
    {
        switch (MenuMgr.Instance.MenuCrudMode)
        {
            case MenuMgr.ECrudMode.None:
                throw new Exception("MenuMgr.Instance.MenuCrudMode == " + MenuMgr.ECrudMode.None.ToString());
            case MenuMgr.ECrudMode.Create:
                TitleLocalizer.Localize("CreateContactsTitle");
                break;
            case MenuMgr.ECrudMode.Update:
                TitleLocalizer.Localize("UpdateContactsTitle");
                break;
            default:
                throw new CaseStatementMissingException(MenuMgr.Instance.MenuCrudMode);
        }
    }

    private string GetTimestampsStr(ContactModel c)
    {
        return string.Format("{0}: {1}{2}{3}: {4}",
            LocalizationMgr.Instance.Translate("CreationTimestamp", true), c.CreationDate.ToString(TimestampFormat),
            Environment.NewLine,
            LocalizationMgr.Instance.Translate("LastUpdatedTimestamp", true), c.UpdatedDate.ToString(TimestampFormat));
    }

    public void LoadContact()
    {
        ContactModel c = ContactToUpdate; // Alias.

        if (c == null)
        {
            TxtTimestamps.text = string.Empty;
            c = new ContactModel();
        }
        else
        {
            TxtTimestamps.text = GetTimestampsStr(c);
        }

        InputName.text = c.Name;
        InputMiddleName.text = c.MiddleName;
        InputSurname.text = c.LastName;
        InputTelephoneNr.text = c.TelephoneNr;
        InputEmail.text = c.Email;
        InputTwitter.text = c.TwitterHandle;
        InputDescription.text = c.Description;
    }

    public void SaveContact()
    {
        if (MenuMgr.Instance.MenuCrudMode == MenuMgr.ECrudMode.Create)
        {
            CreateContact();
        }
        else
        {
            UpdateContact();
        }
        ContactController.Instance.SetAllContactsFromDb_WithFilterSort();
        MenuMgr.Instance.CloseCurrentMenu();
    }

    /// <summary>
    /// TODO: Refactor.
    /// </summary>
    private ContactModel CreateContactFromInputs()
    {
        return new ContactModel(
            DbBaseEntity.InvalidId,
            InputName.text,
            InputMiddleName.text,
            InputSurname.text,
            InputTelephoneNr.text,
            InputDescription.text,
            InputEmail.text,
            InputTwitter.text,
            Db.ActiveUser.Id,
            DateTime.Now,
            DateTime.Now
            );
    }

    private void CreateContact()
    {
        ContactModel newContact = CreateContactFromInputs();
        ContactController.Instance.AllContacts.Add(newContact);
        ContactController.Instance.ReSortAllContacts();
        Db.ContactRepo.Create(newContact);
    }

    /// <summary>
    /// TODO: Refactor.
    /// </summary>
    private void UpdateContact()
    {
        ContactModel c = ContactToUpdate; // Alias.
        
        c.Name = InputName.text;
        c.MiddleName = InputMiddleName.text;
        c.LastName = InputSurname.text;
        c.TelephoneNr = InputTelephoneNr.text;
        c.Email = InputEmail.text;
        c.TwitterHandle = InputTwitter.text;
        c.Description = InputDescription.text;
        c.UpdatedDate = DateTime.Now;

        Db.ContactRepo.Update(c);
    }
}
