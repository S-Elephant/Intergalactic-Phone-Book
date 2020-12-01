using UnityEngine;

public class MenuOpenContactsButton : BaseButton
{
    private ContactModel Contact = null;

    public void Initialize(ContactModel contact)
    {
        this.Contact = contact;
    }

    public override void OnClick()
    {
        Debug.Log("TODO: open new menu for editing contacts. Got contact: " + Contact.GetFullName());
    }
}
