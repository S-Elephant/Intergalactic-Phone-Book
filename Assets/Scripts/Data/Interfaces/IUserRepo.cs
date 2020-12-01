public interface IUserRepo : IRepo<UserModel>
{
    /// <summary>
    /// Also assigns the Id to the UserModel.
    /// </summary>
    bool IsLoginValid(UserModel user);

    /// <summary>
    /// Also assigns the Id to the UserModel.
    /// </summary>
    /// <returns>The Id of the newly created user or a DbBaseEntity.InvalidId if user with the same e-mail already exists.</returns>
    int CreateUser(UserModel user, out string errorWordId);

    /// <returns>true if it exists.</returns>
    bool EmailExists(string email);

    void DeleteUserByEmail(string email);
}
