using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContactCrudSaveButton : BaseButton
{
    [SerializeField] MenuContactsAddEdit MenuScript = null;

    protected override void Start()
    {
        Global.CheckNullValues(gameObject, MenuScript);
        base.Start();
    }

    public override void OnClick()
    {
        MenuScript.SaveContact();
    }
}
