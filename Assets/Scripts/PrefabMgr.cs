using System;
using System.Reflection;
using UnityEngine;

public class PrefabMgr : Singleton<PrefabMgr>
{
#if UNITY_EDITOR
    [Header("IMPORTANT: Load this manager FIRST.")]
    [SerializeField] private bool LogDebugInfo = false;
#endif

    [Header("Graphical User Interface")]
    public GameObject ContactsRow = null;
    public GameObject EventSystem = null;
    public GameObject Popup = null;
    public GameObject TheCanvas = null;
    public GameObject AddEditRow = null;

    [Header("Menus")]
    public GameObject CreateUpdateContactsMenu = null;
    public GameObject ContactsMenu = null;
    public GameObject LoginMenu = null;

    [Header("Debug")]
    public GameObject DebugPrefab = null;

    [Header("Cats")]
    public GameObject AngryCat = null;
    public GameObject CatTerminator = null;
    public GameObject UltraNova = null;

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
