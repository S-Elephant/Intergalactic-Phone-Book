using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This classed is intended for debugging only. I can create default users, fill the database with test data and/or empty the database.
/// Make sure to disable all options (or this entire script) before deploying.
/// Set these values in the Unity Inspector.
/// </summary>
public class DebugDatabase_Sqlite : MonoBehaviour
{
    [Header("Create user (a@a.com // 1a)")]
    [SerializeField] private bool CreateDefaultUser = false;

    [Header("Create user (b@a.com // 1a)")]
    [SerializeField] private bool CreateSecondUser = false;

    [Header("Refil database")]
    [SerializeField] private bool RefillDatabase = false;
    [SerializeField] private int FillRecordCount = 250;

    [Header("Misc")]
    [SerializeField] private bool EmptyDatabase = false;

    private void Awake()
    {
        if (EmptyDatabase || RefillDatabase)
        {
            SqliteHelper.EmptyDatabase();
        }

        if (CreateDefaultUser)
        {
            CreateUser("a@a.com", "1a");
        }

        if (CreateSecondUser)
        {
            CreateUser("b@a.com", "1a");
        }

        if (RefillDatabase)
        {
            int oldId = Db.ActiveUser.Id; // Yeah this Id stuff here is a bit hacky, I know.
            if (!Db.ActiveUser.IsMyIdValid)
            {
                Db.ActiveUser.SetId(DbBaseEntity.FirstValidId);
            }
            List<ContactModel> contacts = ContactModel.CreateRandom(FillRecordCount);
            contacts.ForEach(c => Db.ContactRepo.Create(c)); // This doesn't perform well but this is only executed for a few debugging sessions, it's fine.
            Db.ActiveUser.SetId(oldId);
        }
    }

    private void CreateUser(string email, string unhashedPassword)
    {
        Db.UserRepo.DeleteUserByEmail(email);

        UserModel newUser = new UserModel(email, unhashedPassword, true);
        Db.UserRepo.CreateUser(newUser, out string errorWordId);
        if (errorWordId != null)
        {
            Debug.LogError(LocalizationMgr.Instance.Translate(errorWordId));
        }
    }
}
