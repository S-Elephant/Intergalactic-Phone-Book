namespace Constants
{
    public static class SceneNames
    {
        public const string Main = "MainScene";
        public const string Cat = "CatScene";
    }

    public static class Debug
    {
        public const string StartingSceneName = "InitializationScene";
    }

    public static class Error
    {
        public const string AssignAllInInspector = "Please assign all required fields in the Inspector.";
        public const string LanguageNotAssigned = "Language not assigned.";
    }

    public static class Sqlite
    {
        public const int DatabaseVersion = 1;
        public const string DbFilename = "Db";
    }

    public static class Data
    {
        public const string Null = "<NULL>";
    }

    /// <summary>
    /// Contains PlayerPrefs keys.
    /// </summary>
    public static class Prefs
    {
        public const string DbVersion = "DbVersion";

        public const string RememberEmail = "RememberEmail";
        public const string RememberPassword = "RememberPassword";
        public const string StoredEmail = "StoredEmail";
        public const string StoredPassword = "StoredPassword";

        public const string VolumeMaster = "MasterVolume";
        public const string VolumeSFX = "SFXVolume";
        public const string VolumeMusic = "MusicVolume";
    }

    public static class General
    {
        public const string LocalizationDataFilename = "LocalizationData";
        public const string UnsetString = "<Not set>";
    }
}
