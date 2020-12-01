using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuRegisterNewUser : BaseButton
{
    [SerializeField] private MenuLogin MenuLogin = null;

    protected override void Start()
    {
        Global.CheckNullValues(gameObject, MenuLogin);
        base.Start();
    }

    /// <returns>true if all checks pass.</returns>
    private bool HandleSanityChecks()
    {
        if (MenuLogin.EmailInputField.text == string.Empty)
        {
            PopupMgr.Instance.CreateAndShowPopup("FillInEmailField", PopupMgr.EPopupType.Warning);
            return false;
        }
        if (MenuLogin.PasswordInputField.text == string.Empty)
        {
            PopupMgr.Instance.CreateAndShowPopup("FillInPasswordField", PopupMgr.EPopupType.Warning);
            return false;
        }
        
        return true;
    }

    public override void OnClick()
    {
        RegisterNewUser();
    }

    private void RegisterNewUser()
    {
        if (!HandleSanityChecks()) { return; }

        UserModel newUser = new UserModel(MenuLogin.EmailInputField.text, MenuLogin.PasswordInputField.text, true);
        int newUserId = Db.UserRepo.CreateUser(newUser, out string error_wordId);
        if (error_wordId == null)
        {
            if (newUserId == DbBaseEntity.InvalidId)
            {
                PopupMgr.Instance.CreateAndShowPopup("UnknownError", PopupMgr.EPopupType.Error);
            }
            else
            {
                PopupMgr.Instance.CreateAndShowPopup("UserCreationSuccess");
            }
        }
        else
        {
            PopupMgr.Instance.CreateAndShowPopup(error_wordId);
        }
    }
}
