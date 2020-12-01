using System.IO;
using UnityEngine;

public class CopyDatabase : MonoBehaviour
{
    public TextAsset DatabaseInResource = null;

    /// <summary>
    /// Set to true to copy the database to the PersistentDataStorage,
    /// this will overwrite a previous one if another one with the same name already exists.
    /// </summary>
    [SerializeField] private bool ForceCopy = false;

    private void Awake()
    {
        // Copy if forced or if the database hasn't been copied before on this device.
        if (ForceCopy || !PlayerPrefs.HasKey(Constants.Prefs.DbVersion))
        {
            Global.CheckNullValues(gameObject, DatabaseInResource);
            Copy();
        }
    }

    /// <summary>
    /// Copies the database to the persistent datastorage and saves the current version of the database model to the PlayerPrefs.
    /// By using a version number like this I can, in later versions of this app, overwrite the entire database for everyone if necessary.
    /// </summary>
    private void Copy()
    {
        byte[] data = DatabaseInResource.bytes;
        File.WriteAllBytes(SqliteHelper.FullPersistentDatabasePath, data);
        
        PlayerPrefs.SetInt(Constants.Prefs.DbVersion, Constants.Sqlite.DatabaseVersion);
        PlayerPrefs.Save();
    }
}
