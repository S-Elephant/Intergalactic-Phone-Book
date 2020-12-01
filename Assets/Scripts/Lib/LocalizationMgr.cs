using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

/* XML example:
<?xml version="1.0" encoding="utf-8" ?>
<Language version="1.0.0" languages="gb,nl" defaultLanguage="gb">
	<TheTest gb="Can we park for free at [removed name of company]?" nl="Kunnen we bij [removed name of company] gratis parkeren?"/>
</Language>
*/

/// <summary>
/// This manager automatically translates everything that has a LocalizeMe Script attached and manual translations through code can also be done using GetValue().
/// Usage: LocalizationMgr.Instance.GetValue("CancelButtonText");
/// </summary>
public class LocalizationMgr : Singleton<LocalizationMgr>
{
    #region Properties
    private string _Language = Constants.General.UnsetString;
    [HideInInspector]
    public string Language
    {
        get { return _Language; }
        set
        {
            if (Languages.Contains(value))
            {
                _Language = value;
                if (!string.IsNullOrEmpty(Language))
                {
                    Subscribers.ForEach(l => l.Localize());
                }
            }
            else
            {
                Debug.LogError(string.Format("Received an unknown language. Got: {0}. Have {1}.", value, String.Join(", ", Languages)));
            }
        }
    }

    private List<string> Languages = new List<string>();

    /// <summary>
    /// Dictionary working: Language, WordId, Value
    /// </summary>
    public Dictionary<string, Dictionary<string, string>> Translations { get; private set; } = new Dictionary<string, Dictionary<string, string>>();

    /// <summary>
    /// All subscribers will be automatically localized if the Language is changed.
    /// </summary>
    private readonly List<ILocalizer> Subscribers = new List<ILocalizer>();
    #endregion

    private void Start()
    {
        PopulateFromXml();
    }

    private void PopulateFromXml()
    {
        TextAsset locResource = (TextAsset)Resources.Load(Constants.General.LocalizationDataFilename);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(locResource.text);

        LoadLanguages(doc);

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            if (node.NodeType == XmlNodeType.Comment) { continue; }

            string wordId = node.Name;
            foreach (XmlAttribute attr in node.Attributes)
            {
                string language = attr.Name;
                string translation = attr.Value;
                Translations[language].Add(wordId, translation);
            }
        }
    }

    private void LoadLanguages(XmlDocument doc)
    {
        Languages = doc.DocumentElement.GetAttribute("languages").Split(',').ToList();
        Language = doc.DocumentElement.GetAttribute("defaultLanguage");
        Languages.ForEach(l => Translations.Add(l, new Dictionary<string, string>()));
    }

    /// <summary>
    /// Returns the translated word of the word belonging to the specified wordId.
    /// The language used will be the LocalizationMgr.Language.
    /// </summary>
    public string Translate(string wordId, bool capitalizeFirstChar = false)
    {
        string result;
        if (Language == Constants.General.UnsetString)
        {
            result = "Language not set.";
        } else if (Translations[Language].ContainsKey(wordId))
        {
            result = Translations[Language][wordId];
        }
        else
        {
            Debug.LogError(string.Format("Translation not found: {0}.{1}", Language, wordId));
            result = string.Format("{0} transNotFound.", wordId);
        }

        return capitalizeFirstChar ? CapitalizeFirstChar(result) : result;
    }

    public string TranslateExt(string wordId, bool capitalizeFirstChar = false, string prefix = "", string suffix = "")
    {
        return prefix + Translate(wordId, capitalizeFirstChar) + suffix;
    }

    public string TranslateAsSentence(string wordId)
    {
        return Translate(wordId, true) + ".";
    }

    /// <summary>
    /// Returns a new string with the first character being Capitalized.
    /// If the specified string is null or an empty string then it will return just that.
    /// </summary>
    private string CapitalizeFirstChar(string str)
    {
        switch (str)
        {
            case null: return null;
            case "": return String.Empty;
            default: return str.First().ToString().ToUpper() + str.Substring(1);
        }
    }

    /// <summary>
    /// Subscribes an ILocalizer if it isn't already subscribed.
    /// All subscribers will be automatically localized if the Language is changed.
    /// </summary>
    /// <param name="subscriber"></param>
    public void Subscribe(ILocalizer subscriber)
    {
        if (!Subscribers.Contains(subscriber))
        {
            Subscribers.Add(subscriber);
        }
    }

    /// <summary>
    /// Unsubscribes an ILocalizer.
    /// The unsubscribed ILocalizer will no longer be automatically translated when the language changes.
    /// If the ILocalizer wasn't already subscribed then nothing happens.
    /// </summary>
    /// <param name="subscriber"></param>
    public void UnSubscribe(ILocalizer subscriber)
    {
        Subscribers.Remove(subscriber);
    }
}

/// <summary>
/// I put the interface inside this class because they ALWAYS go together. It may prevent migration (to another project or library or something) errors later on.
/// </summary>
public interface ILocalizer
{
    void Localize();
}
