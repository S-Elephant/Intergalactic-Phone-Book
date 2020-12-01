using Mopsicus.InfiniteScroll;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class MenuFilterContacts : MonoBehaviour
{
    private InputField InputField;
    [SerializeField] private InfiniteScroll InfiniteScrollScript = null;

    private void Start()
    {
        Global.CheckNullValues(gameObject, InfiniteScrollScript);
        InputField = GetComponent<InputField>();
        InputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void OnValueChanged()
    {
        ContactController.Instance.FilterContacts(InputField.text);
        InfiniteScrollScript.InitData(ContactController.Instance.AllContactsFiltered.Count);
    }
}
