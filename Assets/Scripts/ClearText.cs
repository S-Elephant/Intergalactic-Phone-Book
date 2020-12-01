using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clears the text of a Text component so that you can put text on it in the designer for debugging/designing purposes.
/// Just attach this script to a GameObject that has a Text component that you'd like to clear at runtime.
/// </summary>
[RequireComponent(typeof(Text))]
public class ClearText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = string.Empty;
        Destroy(this);
    }
}
