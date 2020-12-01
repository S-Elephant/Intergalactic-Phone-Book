using System;

/// <summary>
/// Attach this script to a button to use that button to change the language used for localization.
/// </summary>
public class SetLanguageByBtn : BaseButton
{
    public string Language = string.Empty;

    public override void OnClick()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        if (Language != string.Empty)
        {
            LocalizationMgr.Instance.Language = Language;
        }
        else
        {
            throw new Exception(Constants.Error.LanguageNotAssigned);
        }
    }
}
