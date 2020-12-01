using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopupMgr : Singleton<PopupMgr>
{
    public enum EPopupType { Information = 0, Warning, Error }

    private readonly List<GameObject> Popups = new List<GameObject>();

    public int MaxPopups = 3;
    private const float PopupSpacing = 5;

    public void CreateAndShowPopup(string wordId, EPopupType popupType = default)
    {
        GameObject popup = CreatePopup(wordId, popupType);

        while (Popups.Count >= MaxPopups)
        {
            RemovePopup(Popups[0]);
        }
        
        Popups.Add(popup);
        
        // Reposition the popups (because they need to be above/below each other.
        if (Popups.Count > 1)
        {
            SetPopupsPosY();
        }
    }

    private GameObject CreatePopup(string wordId, EPopupType popupType)
    {
        GameObject popup = Instantiate(PrefabMgr.Instance.Popup);
        popup.GetComponentInChildren<LocalizeMe>().SetNewWordIdAndLocalize(wordId, true, ".");

        Image image = popup.GetComponentInChildren<Image>();
        switch (popupType)
        {
            case EPopupType.Information:
                image.color = Color.white;
                break;
            case EPopupType.Warning:
                image.color = Color.yellow;
                break;
            case EPopupType.Error:
                image.color = Color.red;
                break;
            default:
                throw new CaseStatementMissingException(popupType);
        }
        return popup;
    }

    /// <summary>
    /// TODO: There's a bug here that also changes the x-position (or something)?
    /// This also occured without Unity animations.
    /// It only happens if a popup got to be removed because of 'overflowing' popups.
    /// It doesn't surprise me that much, this whole thing needs more refactoring, this whole positioning thing is far from ideal.
    /// </summary>
    private void SetPopupsPosY()
    {
        float y = Popups.Last().transform.position.y;
        float offset_Y = Popups[0].GetComponent<Popup>().GetHeight() + PopupSpacing; // TODO: optimize this line.
        Popups.ForEach(p =>
        {
            Popup popupScript = p.GetComponent<Popup>();
            RectTransform rect = popupScript.ImageGameObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
            //rect.localPosition = new Vector3(rect.localPosition.x, y);
            y += offset_Y;
        });
    }

    public void RemovePopup(GameObject popup)
    {
        Popups.Remove(popup);
        Destroy(popup);
    }
}
