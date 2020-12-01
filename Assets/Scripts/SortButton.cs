using UnityEngine;
using static ContactModel;

public class SortButton : BaseButton
{
    [SerializeField] private ESortType SortType = ESortType.None;
    [SerializeField] private ButtonSettings MenuButtonScript = null;

    protected override  void Start()
    {
        Global.CheckNullValues(gameObject, SortType, MenuButtonScript);
        base.Start();
    }

    public override void OnClick()
    {
        // Sort.
        ContactController.Instance.SortAllContacts(SortType);
        // Refresh.
        ((MenuContacts)MenuMgr.Instance.GetCurrentMenuScript()).RefreshAllContacts();
        // Close the sorting menu.
        MenuButtonScript.CloseSortingMenu();
    }
}
