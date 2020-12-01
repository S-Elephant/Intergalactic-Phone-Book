public class CatExitButton : BaseButton
{
    public override void OnClick()
    {
        CatMgr.Instance.Exit();
    }
}
