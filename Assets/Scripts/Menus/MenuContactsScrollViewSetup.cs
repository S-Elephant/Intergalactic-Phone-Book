using Mopsicus.InfiniteScroll;
using System;
using UnityEngine;

[RequireComponent(typeof(InfiniteScroll))]
public class MenuContactsScrollViewSetup : MonoBehaviour
{
	[SerializeField] private MenuContacts MenuContacts = null;

	private void Start()
	{
		Global.CheckNullValues(gameObject, MenuContacts);
		ContactController.Instance.FilterContacts(string.Empty, true);

		InfiniteScroll scroll = GetComponent<InfiniteScroll>();
		scroll.OnFill += OnFillItem;
		scroll.OnHeight += OnHeightItem;

		scroll.InitData(ContactController.Instance.AllContactsFiltered.Count);
	}

	private void OnFillItem(int index, GameObject item)
	{
		item.GetComponentInChildren<MenuContactsButton>().Initialize(ContactController.Instance.AllContactsFiltered[index], MenuContacts);
	}

	private int OnHeightItem(int index)
	{
		return 48;
	}
}
