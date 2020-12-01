public static class Db
{
    /// <summary>
    /// The currently logged in user. If no user is logged in then this will contain an empty user.
    /// </summary>
    public static UserModel ActiveUser = UserModel.CreateEmpty();

    /// <summary>
    /// The User Repository.
    /// </summary>
    public static IUserRepo UserRepo { get; private set; } = new UserImp_Sqlite();

    /// <summary>
    /// The Contact Repository.
    /// </summary>
    public static IContactRepo ContactRepo { get; private set; } = new ContactImp_Sqlite();
}
