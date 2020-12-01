using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

/// <summary>
/// Refactor note: There's probably an automated way to map objects to/from Sqlite in mono C#? Due to time contraints I do it manually.
/// </summary>
public class ContactImp_Sqlite : ContactBaseImpl, IContactRepo
{
    /// <summary>
    /// Maps reader data to a new ContactModel.
    /// </summary>
    /// <param name="r">This will not be disposed and it must already have read the entry.</param>
    /// <returns>The mapped ContactModel</returns>
    private ContactModel MapContact(IDataReader r)
    {
        // For performance reasons just get them by index.
        return new ContactModel(
                            r.GetInt32(0), // Id
                            r.GetString(1), // Name
                            r.GetString(2), // MiddleName
                            r.GetString(3), // LastName
                            r.GetString(4), // Tel
                            r.GetString(5), // Desc
                            r.GetString(6), // E-mail
                            r.GetString(7), // Twitter
                            r.GetInt32(8), // Owner
                            DateTime.Parse(r.GetString(9)), // Creation
                            DateTime.Parse(r.GetString(10)) // Updated
                            );
    }

    /// <summary>
    /// Note that this method will also set the correct Id in the contact itself
    /// </summary>
    public int Create(ContactModel contact)
    {
        CheckOperationAllowed(contact);

        using (IDbConnection con = SqliteHelper.CreateConnection())
        {
            using (IDbCommand cmd = con.CreateCommand())
            {
                #region Create the command text & parameters dynamically
                List<FieldInfo> allFields = contact.GetAllFields();
                StringBuilder cmdText = new StringBuilder("INSERT INTO Contact VALUES(@pId, ");
                allFields.ForEach(f =>
                {
                    cmdText.Append(string.Format("@p{0}, ", f.Name));
                    cmd.Parameters.Add(new SqliteParameter(string.Format("@p{0}", f.Name), f.GetValue(contact)));
                });
                cmdText.Length -= 2; // Remove the last ", "
                cmdText.Append(")");
                cmd.CommandText = cmdText.ToString();
                #endregion
                cmd.Parameters.Add(new SqliteParameter("@pId", null));
                SqliteHelper.ExecuteNonQuery(con, cmd);
            }

            using (IDbCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT Id FROM Contact ORDER BY Id DESC LIMIT 1";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                contact.SetId(id);
                return id;
            }
        }
    }

    public void Delete(ContactModel contact)
    {
        CheckOperationAllowed(contact);
        if (contact.Id < 0) { throw new Exception(string.Format("Received an invalid Id. Got: {0}.", contact.Id)); }

        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            cmd.CommandText = "DELETE FROM Contact WHERE Id=@pId";
            cmd.Parameters.Add(new SqliteParameter("@pId", contact.Id));
        }
    }

    public List<ContactModel> ReadAll()
    {
        if (!DbBaseEntity.IsIdValid(Db.ActiveUser.Id)) { return new List<ContactModel>(); }

        List<ContactModel> allContacts = new List<ContactModel>();
        using (IDbConnection con = SqliteHelper.CreateConnection())
        {
            using (IDbCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Contact WHERE Owner=@pOwner";
                cmd.Parameters.Add(new SqliteParameter("@pOwner", Db.ActiveUser.Id));
                con.Open();
                using (IDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        allContacts.Add(MapContact(r));
                    }
                }
            }

            con.Close();
        }
        return allContacts;
    }

    public void Update(ContactModel contact)
    {
        CheckOperationAllowed(contact);

        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            List<FieldInfo> allFields = contact.GetAllFields();
            #region Create the command text & parameters dynamically
            StringBuilder cmdText = new StringBuilder("UPDATE Contact SET ");
            allFields.ForEach(f =>
            {
                cmdText.Append(string.Format("{0}=@p{0}, ", f.Name));
                cmd.Parameters.Add(new SqliteParameter(string.Format("@p{0}", f.Name), f.GetValue(contact)));
            });
            cmdText.Length -= 2; // Remove the last ", "
            cmdText.Append(" WHERE ID=@pId AND Owner=@pOwner");
            cmd.CommandText = cmdText.ToString();
            cmd.Parameters.Add(new SqliteParameter("@pId", contact.Id));
            #endregion
            SqliteHelper.ExecuteNonQuery(con, cmd);
        }
    }
}
