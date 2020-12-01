public class MenuContactCrudCancelButton : BaseButton
{
    public override void OnClick()
    {
        MenuMgr.Instance.CloseCurrentMenu();
    }
}
