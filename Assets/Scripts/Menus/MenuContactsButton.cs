using UnityEngine.UI;

public class MenuContactsButton : BaseButton
{
    private ContactModel Contact = null;
    private MenuContacts MenuContactsScript = null;

    public void Initialize(ContactModel contact, MenuContacts menuContactsScript)
    {
        this.Contact = contact;
        MenuContactsScript = menuContactsScript;
        GetComponentInChildren<Text>().text = contact.GetFullName();
    }

    public override void OnClick()
    {
        MenuContactsScript.SetSelectedContact(this.Contact);
    }
}
