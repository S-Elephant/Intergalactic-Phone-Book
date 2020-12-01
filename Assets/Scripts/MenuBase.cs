using UnityEngine;

/// <summary>
/// All menus classes MUST inherit from this.
/// </summary>
public abstract class MenuBase : MonoBehaviour
{
    public virtual void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
