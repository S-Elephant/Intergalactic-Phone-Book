using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject ImageGameObj = null;
    private const float AnimationDuration = 3f;
    private float TimeLeft = 3;

    private void Start()
    {
        Global.CheckNullValues(gameObject, ImageGameObj);
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0)
        {
            PopupMgr.Instance.RemovePopup(this.gameObject);
        }
    }

    public float GetHeight()
    {
        return ImageGameObj.GetComponent<RectTransform>().rect.height;
    }
}
