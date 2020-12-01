using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddEditRow : MonoBehaviour
{
    [SerializeField] private LocalizeMe TextLocalizer = null;
    [SerializeField] private LocalizeMe InputPlaceholderLocalizer = null;
    public InputField TheInputField = null;

    private void Start()
    {
        Global.CheckNullValues(gameObject, TextLocalizer, InputPlaceholderLocalizer, TheInputField);
    }

    public void Initialize(string textWordID, string inputPlaceholderWordId)
    {
        TextLocalizer.SetNewWordIdAndLocalize(textWordID);
        InputPlaceholderLocalizer.SetNewWordIdAndLocalize(inputPlaceholderWordId, suffix: "...");
    }
}
