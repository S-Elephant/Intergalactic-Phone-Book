using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this script to any Component to translate it's first Text component found.
/// It can also be attached to a GameObject w/o a TextComponent but then make sure to 
/// set Recursive to true and then one of the children must have a Text component.
/// </summary>
public class LocalizeMe : MonoBehaviour, ILocalizer
{
    public string WordId;

    /// <summary>
    /// This will be added behind the translated text.
    /// Use this to add suffixes like for example: "?", "." or "..." without having to
    /// create a whole new localization entry for each one.
    /// </summary>
    public string Suffix = string.Empty;

    /// <summary>
    /// If true then the first character of the translated word will be capitalized.
    /// </summary>
    public bool CapitalizeFirstChar = true;

    /// <summary>
    /// Do this recursively for all Text Components?
    /// </summary>
    public bool Recursive = false;

    private bool AppIsShuttingDown = false;

    private void Awake()
    {
        Localize();
        LocalizationMgr.Instance.Subscribe(this);
    }

    public void SetNewWordIdAndLocalize(string wordId, bool capitalizeFirstChar = true, string suffix = "")
    {
        this.WordId = wordId;
        this.CapitalizeFirstChar = capitalizeFirstChar;
        this.Suffix = suffix;
        Localize();
    }

    public void Localize(string wordID)
    {
        this.WordId = wordID;
        Localize();
    }

    public void Localize()
    {
        if (!string.IsNullOrEmpty(WordId))
        {
            List<Text> texts = Recursive ? GetComponentsInChildren<Text>().ToList() : new List<Text>() { GetComponent<Text>() };
            if (texts.Count == 0) { throw new NullReferenceException(string.Format("Unable to locate Text component. Wordid: {0}.", WordId)); }
            texts.ForEach(t => t.text = LocalizationMgr.Instance.TranslateExt(WordId, CapitalizeFirstChar, suffix: Suffix));
        }
    }

    private void OnApplicationQuit()
    {
        AppIsShuttingDown = true;
    }

    private void OnDestroy()
    {
        if (!AppIsShuttingDown && LocalizationMgr.Instance != null)
        {
            LocalizationMgr.Instance.UnSubscribe(this);
        }
    }
}
