using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSettings : BaseButton
{
    public Animator SettingsAnimator = null;
    private bool SlideIn = false;
    protected override void Start()
    {
        Global.CheckNullValues(gameObject, SettingsAnimator);
        base.Start();
    }

    public override void OnClick()
    {
        ToggleSortingMenu();
    }

    private void ToggleSortingMenu()
    {
        SlideIn = !SlideIn;
        SettingsAnimator.SetBool("SlideIn", !SettingsAnimator.GetBool("SlideIn"));
    }

    /// <summary>
    /// This extra method prevents the sorting buttons from re-opening the menu again.
    /// </summary>
    public void CloseSortingMenu()
    {
        SlideIn = false;
        SettingsAnimator.SetBool("SlideIn", !SettingsAnimator.GetBool("SlideIn"));
    }
}
