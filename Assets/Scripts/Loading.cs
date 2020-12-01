using System;
using UnityEngine;

/// <summary>
/// If the remote connection is enabled, this screen will wait for <c>RemoteConnectionTimeOut</c> seconds.
/// 
/// TODO: Refactor: 
/// This should be made more generic. Also it should be possible to transform the animation into
/// a slide-in AND slide-out at the same time using a negative speed. After that convert it into a prefab.
/// Alternatively I could make a manager singleton that handles all different kind of screen transition animations for every screen, this is likely better.
/// </summary>
public class Loading : MonoBehaviour
{
    private GotoScene GotoSceneScript = null;
    [SerializeField] private GameObject TransitionGo = null;
    
    /// <summary>
    /// In seconds.
    /// </summary>
    [Range(0,60)]
    [SerializeField] float RemoteConnectionTimeOut = 3;
    
    /// <summary>
    /// How much time (in seconds) is remaining before the time-out is triggered.
    /// </summary>
    private float TimeRemaining;

    private bool ProcessUpdate = true;

    private void Start()
    {
        Global.CheckNullValues(gameObject, TransitionGo);
        TransitionGo.GetComponentInChildren<OnAnimationFinish>().OnFinish += Animation_OnFinish;

        GotoSceneScript = GetComponent<GotoScene>();
        TimeRemaining = RemoteConnectionTimeOut;
    }

    private void Animation_OnFinish(object sender)
    {
        GotoSceneScript.CanGotoNextScene = true;
    }

    private void Update()
    {
        if (ProcessUpdate)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0 || !RestClient.Instance.IsEnabled || RestClient.Instance.DataRetrievalComplete)
            {
                TransitionGo.SetActive(true);
                ProcessUpdate = false;
            }
        }
    }
}
