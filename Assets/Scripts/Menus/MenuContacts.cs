using Mopsicus.InfiniteScroll;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuContacts : MenuBase
{
    public InfiniteScroll InfiniteScrollScript = null;
    public ContactModel SelectedContact { get; private set; } = null;
    [SerializeField] private Text TxtFullName = null;
    [SerializeField] private Text TxtTelephoneNr = null;
    [SerializeField] private Text TxtEmail = null;
    [SerializeField] private Button EditBtn = null;
    private bool StartHasBeenCalled = false;

    private void Start()
    {
        Global.CheckNullValues(gameObject, TxtFullName, TxtTelephoneNr, InfiniteScrollScript, EditBtn, TxtEmail);
        StartHasBeenCalled = true;
    }

    public void SetSelectedContact(ContactModel contact)
    {
        this.SelectedContact = contact;
        DisplayContactInfo();
    }

    private void DisplayContactInfo()
    {
        if (SelectedContact == null)
        {
            TxtFullName.text = TxtTelephoneNr.text = TxtEmail.text = string.Empty;
            EditBtn.interactable = false;
        }
        else
        {
            TxtFullName.text = SelectedContact.GetFullName();
            TxtTelephoneNr.text = SelectedContact.TelephoneNr;
            TxtEmail.text = SelectedContact.Email;
            EditBtn.interactable = true;
        }
    }

    public void RefreshAllContacts()
    {
        if (StartHasBeenCalled)
        {
            InfiniteScrollScript.InitData(ContactController.Instance.AllContactsFiltered.Count);
        }
    }

    public override void Activate()
    {
        base.Activate();
        RefreshAllContacts();
        SetSelectedContact(null); // This line prevents odd behavior and bugs after adding and/or deleting an item not to mention having an item selected that isn't visible/loaded in the scrollview.
    }
}
