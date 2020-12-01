#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Usage example: [DisplayName("my readable string"] public string SomeAnnoyingPropretyName;
/// </summary>
public class DisplayNameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public DisplayNameAttribute(string name)
    {
        NewName = name;
    }
}

/// <summary>
/// https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html
/// </summary>
[CustomPropertyDrawer(typeof(DisplayNameAttribute))]
public class DisplayNameEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent((attribute as DisplayNameAttribute).NewName));
    }
}
#endif
