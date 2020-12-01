/// <summary>
/// This class should only be used for debugging.
/// </summary>
public class UserImp_HC : IUserRepo
{
    public int CreateUser(UserModel user, out string errorWordId)
    {
        errorWordId = null;
        return DbBaseEntity.FirstValidId;
    }

    public void DeleteUserByEmail(string email)
    {
        // Do nothing.
    }

    public bool EmailExists(string email)
    {
        return true;
    }

    public bool IsLoginValid(UserModel user)
    {
        user.SetId(DbBaseEntity.FirstValidId);

        return true;
    }
}
