#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

/* Well f... none is working: https://answers.unity.com/questions/441246/editor-script-to-make-play-always-jump-to-a-start.html
 * Code below is written by me.
 */

/// <summary>
/// Allows me to run the project from a starting scene w/o having to actually open that Scene in Unity myself. Afterwards it also reopens the scene I was working on.
/// Just press CTRL + P to use this and assign the Constants.Debug.StartingSceneName.
/// Running this for the first time might throw a Unity Shortcut Conflict (just resolve it to this one) or change the shortcut keys manually before using the hotkeys the first time.
/// This should be stored in some kind of preference or whatever but I got not time for that, only 7 days... Kinda reminds me of https://store.steampowered.com/app/251570/7_Days_to_Die/
/// </summary>
public class PlayFromScene
{
    private const string PREF_STR = "LastScene";

    public static string LastScenePath
    {
        get { return EditorPrefs.HasKey(PREF_STR) ? EditorPrefs.GetString(PREF_STR) : string.Empty; }
        set { EditorPrefs.SetString(PREF_STR, value); }
    }

    // Note: You might want to go to Edit > Shortcuts and search for "play" and remove the ctrl + P from the original play button.
    [MenuItem("Play/Execute starting scene _%p")]
    public static void RunMainScene()
    {
        //Debug.LogWarning(LastScenePath);
        if (LastScenePath == string.Empty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            LastScenePath = EditorSceneManager.GetActiveScene().path.ToString();
            EditorSceneManager.OpenScene("Assets/Scenes/" + Constants.Debug.StartingSceneName + ".unity");
            EditorApplication.isPlaying = true;
        }
        else
        {
            EditorSceneManager.OpenScene(LastScenePath);
            LastScenePath = string.Empty;
        }
    }
}

[InitializeOnLoadAttribute] // Ensure class initializer is called whenever scripts recompile
public class ReturnToOriginalScene
{
    // register an event handler when the class is initialized
    static ReturnToOriginalScene()
    {
        EditorApplication.playModeStateChanged += TryToReturn;
    }

    private static void TryToReturn(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode && PlayFromScene.LastScenePath != string.Empty)
        {
            EditorSceneManager.OpenScene(PlayFromScene.LastScenePath);
            PlayFromScene.LastScenePath = string.Empty;
        }
    }
}
#endif