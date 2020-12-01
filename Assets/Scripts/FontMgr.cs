using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// This class is responsible for switching fonts.
/// </summary>
public class FontMgr : Singleton<FontMgr>
{
#if UNITY_EDITOR
    [SerializeField] private bool LogDebugInfo = false;
#endif

    public Font ActiveFont = null;
    public Font DefaultFont = null;
    public Font FutureSpore = null;

    protected override void Awake()
    {
        base.Awake();
        DoNullChecks();
    }

    /// <summary>
    /// This method raises an exception if any public variable found in this class its instance converts to "null".
    /// </summary>
    private void DoNullChecks()
    {
        BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance |
                            //System.Reflection.BindingFlags.NonPublic | // The non-public ones don't require a sanity check.
                            System.Reflection.BindingFlags.Public;

        string errorList = string.Empty;
        foreach (FieldInfo fieldInfo in this.GetType().GetFields(bindingFlags))
        {
            if (fieldInfo.GetValue(this).ToString() == "null")
            {
                errorList = string.Format("{0}{1}{2}", errorList, Environment.NewLine, fieldInfo.Name);
            }
#if UNITY_EDITOR
            else if (LogDebugInfo)
            {
                Debug.Log(string.Format("Name: {0}, Type: {1}, Value: {2}", fieldInfo.Name, fieldInfo.GetValue(this), (fieldInfo.GetValue(this)).GetType()));
            }
#endif
        }

        if (errorList != string.Empty)
        {
            throw new NullReferenceException("The following public variables are null:" + errorList);
        }
    }
}
