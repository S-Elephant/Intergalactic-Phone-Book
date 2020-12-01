#if !UNITY_EDITOR
using UnityEngine;
#endif
using UnityEngine.SceneManagement;

public class CatButton : BaseButton
{
    public override void OnClick()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(Constants.SceneNames.Cat);
#else
        if (SystemInfo.supportsGyroscope)
        {
            SceneManager.LoadScene(Constants.SceneNames.Cat);
        }
        else
        {
            PopupMgr.Instance.CreateAndShowPopup("GyroNotSupported", PopupMgr.EPopupType.Warning);
        }
#endif
    }
}
