using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuLogin : MenuBase
{
    public InputField EmailInputField = null;
    public InputField PasswordInputField = null;
    public Toggle RememberEmailToggle = null;
    public Toggle RememberPasswordToggle = null;
    /// <summary>
    /// The ScrollView gameobject that is used for a possible REST error.
    /// </summary>
    public GameObject RestErrorGo = null;
    [SerializeField] private GameObject TxtVersion = null;
    [SerializeField] private Text TxtRestStatus = null;

    private void Start()
    {
        if (PassesSanityChecks())
        {
            TxtVersion.GetComponent<Text>().text = string.Format("v{0} ({1})", Application.version, Application.unityVersion);
        }

        RememberEmailToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(Constants.Prefs.RememberEmail, 1));
        RememberPasswordToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(Constants.Prefs.RememberPassword, 0));
        RestoreEntries();

        if (RestClient.Instance.IsEnabled)
        {
            if (RestClient.Instance.Result != null)
            {
                SetRestStatus(RestClient.Instance.Success, RestClient.Instance.Result);
            }
            else
            {
                RestClient.Instance.OnResult += Instance_OnResult;
            }
        }
        else
        {
            TxtRestStatus.GetComponent<LocalizeMe>().SetNewWordIdAndLocalize("RemoteConnectionDisabled", suffix: ".");
        }
    }

    private void SetRestStatus(bool success, string result)
    {
        if (success)
        {
            TxtRestStatus.GetComponent<LocalizeMe>().SetNewWordIdAndLocalize("ConnectedToRemote", suffix: ".");
        }
        else
        {
            TxtRestStatus.GetComponent<LocalizeMe>().SetNewWordIdAndLocalize("ConnectionFailed", suffix: ".");
            RestErrorGo.SetActive(true);
            RestErrorGo.GetComponentInChildren<Text>().text = result;
        }
        
    }

    private void Instance_OnResult(bool success, string jsonResult)
    {
        if (success)
        {
            SetRestStatus(success, jsonResult);
        }
        RestClient.Instance.OnResult -= Instance_OnResult;
    }

    public override void Deactivate()
    {
        RestClient.Instance.OnResult -= Instance_OnResult;
        base.Deactivate();
    }

    private void RestoreEntries()
    {
        if (RememberEmailToggle.isOn)
        {
            EmailInputField.text = EncryptionController.Encryptor.Decrypt(PlayerPrefs.GetString(Constants.Prefs.StoredEmail, string.Empty));
            PasswordInputField.text = EncryptionController.Encryptor.Decrypt(PlayerPrefs.GetString(Constants.Prefs.StoredPassword, string.Empty));
        }
    }

    private bool PassesSanityChecks()
    {
        bool success = true;
        if (TxtVersion == null || EmailInputField == null || PasswordInputField == null || RestErrorGo == null ||
            RememberEmailToggle == null || RememberPasswordToggle == null || TxtRestStatus == null)
        {
            Debug.LogError("One or more properties are null. Please assign them in the Inspector.");
            success = false;
        }
        return success;
    }
}
