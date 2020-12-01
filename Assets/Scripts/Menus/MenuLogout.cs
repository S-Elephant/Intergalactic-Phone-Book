public class MenuLogout : BaseButton
{
    public override void OnClick()
    {
        Logout();
    }

    private void Logout()
    {
        Db.ActiveUser = UserModel.CreateEmpty();
        MenuMgr.Instance.CloseCurrentMenu();
    }
}
