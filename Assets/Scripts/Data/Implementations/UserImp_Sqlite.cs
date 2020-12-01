using Mono.Data.Sqlite;
using System;
using System.Data;

public class UserImp_Sqlite : IUserRepo
{
    /// <summary>
    /// Note that the user.Password should already have been hashed.
    /// </summary>
    public bool IsLoginValid(UserModel user)
    {
        if (!EmailExists(user.Email))
        {
            user.SetId(DbBaseEntity.InvalidId);
            return false; // Note: This is efficient but hackers could measure the time it takes to return an invalid e-mail address. However, for this project such security measures are rather overkill.
        }

        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            cmd.CommandText = "SELECT Id FROM User WHERE Email=@pEmail AND Password=@pPassword";
            cmd.Parameters.Add(new SqliteParameter("@pEmail", user.Email));
            cmd.Parameters.Add(new SqliteParameter("@pPassword", user.Password));
            
            object idFromDb = Convert.ToInt32(SqliteHelper.ExecuteScalar(con, cmd));
            
            if (idFromDb == null)
            {
                user.SetId(DbBaseEntity.InvalidId);
                return false;
            }
            else
            {
                user.SetId(Convert.ToInt32(idFromDb));
                return DbBaseEntity.IsIdValid(user.Id);
            }
        }
    }

    /// <param name="user">Note that it assumes that the user.Password has been hashed or it will most likely return false.</param>
    /// <param name="errorWordId">null if no error occured (or none was caught); otherwise returns the translation key for the error.</param>
    /// <returns>If the user was created the Id of the user; otherwise returns DbBaseEntity.InvalidId.</returns>
    public int CreateUser(UserModel user, out string errorWordId)
    {
        errorWordId = null;
        if (!EmailExists(user.Email))
        {
            using (IDbConnection con = SqliteHelper.CreateConnection())
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO User VALUES(@pId, @pEmail, @pPassword)";
                    cmd.Parameters.Add(new SqliteParameter("@pId", null));
                    cmd.Parameters.Add(new SqliteParameter("@pEmail", user.Email.ToLower()));
                    cmd.Parameters.Add(new SqliteParameter("@pPassword", user.Password));
                    SqliteHelper.ExecuteNonQuery(con, cmd);
                }

                using (IDbCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT Id FROM User ORDER BY Id DESC LIMIT 1";
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    user.SetId(id);
                    return id;
                }
            }
        }
        else
        {
            errorWordId = "EmailAlreadyExists";
        }
        return DbBaseEntity.InvalidId;
    }

    public bool EmailExists(string email)
    {
        email = email.ToLower();

        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            cmd.CommandText = "SELECT COUNT(*) FROM User WHERE Email=@pEmail";
            cmd.Parameters.Add(new SqliteParameter("@pEmail", email));
            con.Open();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return result == 1;
        }
    }

    public void DeleteUserByEmail(string email)
    {
        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM User WHERE Email=@pEmail";
            cmd.Parameters.Add(new SqliteParameter("@pEmail", email));
            SqliteHelper.ExecuteNonQuery(con, cmd);
        }
    }
}
