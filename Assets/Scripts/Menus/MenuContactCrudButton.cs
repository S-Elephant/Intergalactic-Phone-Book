using System;
using UnityEngine;

public class MenuContactCrudButton : BaseButton
{
    [SerializeField] private MenuMgr.ECrudMode MenuCrudMode = MenuMgr.ECrudMode.None;
    [SerializeField] MenuContacts MenuScript = null;

    protected override void Start()
    {
        Global.CheckNullValues(gameObject, MenuScript);
        base.Start();
    }

    public override void OnClick()
    {
        if (MenuCrudMode == MenuMgr.ECrudMode.None) { throw new Exception("Assign MenuCrudmode in the Inspector."); }
        MenuMgr.Instance.MenuCrudMode = this.MenuCrudMode;
        MenuContactsAddEdit.ContactToUpdate = MenuScript.SelectedContact;
        MenuMgr.Instance.CreateAndAddMenu(PrefabMgr.Instance.CreateUpdateContactsMenu);
    }
}
