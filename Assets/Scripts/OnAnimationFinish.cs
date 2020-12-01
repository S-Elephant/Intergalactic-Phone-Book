using UnityEngine;

public class OnAnimationFinish : MonoBehaviour
{
    public delegate void OnFinishHandler(object sender);
    public event OnFinishHandler OnFinish;

    private void Start()
    {
        OnFinish += OnAnimationFinish_OnFinish;
    }

    private void OnAnimationFinish_OnFinish(object sender)
    {
        GetComponent<Animator>().speed = 0f; // Pause the animation. I don't know how to make it stop in Unity itself.
    }

    private void RaiseOnFinish()
    {
        OnFinish?.Invoke(this);
    }
}
