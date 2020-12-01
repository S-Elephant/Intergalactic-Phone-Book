using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : Singleton<RestClient>
{
    /// <summary>
    /// Set to false to disable this class. For debugging purposes only.
    /// </summary>
    #if UNITY_EDITOR
    [DisplayName("IsEnabled")]
    #endif
    [SerializeField] private bool _IsEnabled = false;
    public bool IsEnabled { get { return _IsEnabled; } }

    /// <summary>
    /// The full URL to the REST Web API. Example: http://192.168.178.10/IPB_BackEnd/Contact
    /// </summary>
    public string ContactUrl = "http://192.168.178.10/IPB_BackEnd/Contact";

    public string UserUrl = "http://192.168.178.10/IPB_BackEnd/User";

    [HideInInspector] public bool Success { get; private set; } = false;
    [HideInInspector] public string Result = null;

    public delegate void OnResultEventHandler(bool success, string jsonResult);
    public event OnResultEventHandler OnResult;

    public bool DataRetrievalComplete { get; private set; } = false;

    private void Start()
    {
        if (IsEnabled)
        {
            StartCoroutine(Get(ContactUrl));
        }
        else
        {
            Result = LocalizationMgr.Instance.TranslateAsSentence("RemoteConnectionDisabled");
        }
    }

    public IEnumerator Get(string url)
    {
        DataRetrievalComplete = false;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
                Result = www.error;
                DataRetrievalComplete = true;
                Success = false;
                RaiseOnResult(false);
            }
            else
            {
                if (www.isDone)
                {
                    Result = Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log("Result from back-end:" + Result);
                    DataRetrievalComplete = true;
                    Success = true;
                    RaiseOnResult(true);
                }
            }
        }
    }

    private void RaiseOnResult(bool success)
    {
        OnResult?.Invoke(success, Result);
    }
}
