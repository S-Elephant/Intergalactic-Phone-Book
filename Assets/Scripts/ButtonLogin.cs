using System;
using UnityEngine;

public class ButtonLogin : BaseButton
{
    [SerializeField] private MenuLogin MenuLogin = null;

    protected override void Start()
    {
        Global.CheckNullValues(gameObject, MenuLogin);
        base.Start();
    }

    private void HandleEntries()
    {
        PlayerPrefs.SetInt(Constants.Prefs.RememberEmail, Convert.ToInt32(MenuLogin.RememberEmailToggle.isOn));
        PlayerPrefs.SetInt(Constants.Prefs.RememberPassword, Convert.ToInt32(MenuLogin.RememberPasswordToggle.isOn));

        if (MenuLogin.RememberEmailToggle.isOn) { PlayerPrefs.SetString(Constants.Prefs.StoredEmail, EncryptionController.Encryptor.Encrypt(MenuLogin.EmailInputField.text)); }
        else { MenuLogin.EmailInputField.text = string.Empty; }

        if (MenuLogin.RememberPasswordToggle.isOn) { PlayerPrefs.SetString(Constants.Prefs.StoredPassword, EncryptionController.Encryptor.Encrypt(MenuLogin.PasswordInputField.text)); } // TODO: Yes this needs encryption.
        else { MenuLogin.PasswordInputField.text = string.Empty; }

        PlayerPrefs.Save();
    }

    public void Login()
    {
        UserModel user = new UserModel(MenuLogin.EmailInputField.text, MenuLogin.PasswordInputField.text, true);
        if (Db.UserRepo.IsLoginValid(user))
        {
            Db.ActiveUser = user;
            ContactController.Instance.SetAllContactsFromDb(true);
            HandleEntries();
            MenuMgr.Instance.CreateAndAddMenu(PrefabMgr.Instance.ContactsMenu);
        }
        else
        {
            PopupMgr.Instance.CreateAndShowPopup("ErrorLoginCreds", PopupMgr.EPopupType.Error);
        }
    }

    public override void OnClick()
    {
        Login();
    }
}
