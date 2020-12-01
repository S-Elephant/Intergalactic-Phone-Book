using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public bool CanGotoNextScene = true;
    public string SceneName = null;

    private void Start()
    {
        StartCoroutine(AttemptToGotoNextScene());
    }

    private IEnumerator AttemptToGotoNextScene()
    {
        while (!CanGotoNextScene)
        {
            yield return new WaitForSeconds(1);
        }

        if (SceneName != null && SceneName != string.Empty)
        {
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            Debug.LogError("GotoThisScene is null.");
        }
    }
}
