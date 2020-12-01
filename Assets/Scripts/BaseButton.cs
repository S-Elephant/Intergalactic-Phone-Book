using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inherit from this class to automatically register to the button's OnClick event.
/// </summary>
[RequireComponent(typeof(Button))]
public abstract class BaseButton : MonoBehaviour
{
    /// <summary>
    /// Important: Don't forget to call base.Start(); if you override this Start method.
    /// </summary>
    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public abstract void OnClick();
}
