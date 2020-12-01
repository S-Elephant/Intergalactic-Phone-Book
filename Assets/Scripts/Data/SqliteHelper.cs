using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

/// <summary>
/// <Removed name of company> notes:
/// Having a static helper like this is imo still a Repository DP. However, I understand that some people believe this to be an anti-pattern.
/// Either way, I find this a useful (if not valid) Repository PD because I can still easily attach a totally different database to this app.
/// Also not every form of persistent datastorage requires a connectionstring and such thus adding another interface or container for that imo is also not 100.0% proper.
/// I also just don't have enough time + it makes no sense to design it too heavy for such a small app (it's already too heavy, but for the sake of demonstration).
/// </summary>
public class SqliteHelper : Singleton<SqliteHelper>
{
    /// <summary>
    /// Returns the full path to the persistent database path. This includes the drive-letter, directories, filename and extension.
    /// Note about the file extension: I currently probably use no extension at all in the Constants.
    /// </summary>
    public static string FullPersistentDatabasePath { get { return Path.Combine(Application.persistentDataPath, Constants.Sqlite.DbFilename); } }

    /// <summary>
    /// The SQlite connection string to connect to the database located in the persistent datastorage.
    /// </summary>
    public static string ConnectionString { get { return string.Format("URI=file:{0}", FullPersistentDatabasePath); } }

    /// <summary>
    /// IMPORTANT: After done using this, make sure to dispose the returned IDbConnection.
    /// </summary>
    /// <returns>A closed IDbConnection</returns>
    public static IDbConnection CreateConnection()
    {
        return new SqliteConnection(ConnectionString);
    }

    /// <summary>
    /// A wrapper for executing a query with opening and closing the connection.
    /// DOESN'T dispose anything.
    /// </summary>
    public static void ExecuteNonQuery(IDbConnection con, IDbCommand cmd)
    {
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    /// <summary>
    /// A wrapper for executing a query with opening and closing the connection.
    /// DOESN'T dispose anything.
    /// </summary>
    public static object ExecuteScalar(IDbConnection con, IDbCommand cmd)
    {
        con.Open();
        object result = cmd.ExecuteScalar();
        con.Close();
        return result;
    }

    /// <returns>Returns the names of all tables in the database excluding the ones from Sqlite.</returns>
    private static List<string> GetTableNames()
    {
        List<string> tableNames = new List<string>();
        using (IDbConnection con = SqliteHelper.CreateConnection())
        {
            using (IDbCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = @"SELECT name FROM sqlite_master WHERE type = 'table'";
                con.Open();
                using (IDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tableNames.Add(r.GetString(0));
                    }
                }
            }

            con.Close();
        }
        return tableNames;
    }

    /// <summary>
    /// Empties the entire database (excluding the table(s) from Sqlite).
    /// Therefor this will also delete all users.
    /// </summary>
    public static void EmptyDatabase()
    {
        using (IDbConnection con = SqliteHelper.CreateConnection())
        using (IDbCommand cmd = con.CreateCommand())
        {
            List<string> tableNames = GetTableNames();
            con.Open();
            tableNames.ForEach(t =>
            {
                cmd.CommandText = string.Format("DELETE FROM {0}", t); // string.Format() instead of a parameter because it's not a value.
                cmd.ExecuteNonQuery();
            });
            con.Close();
        }
    }
}
