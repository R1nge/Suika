using System.IO;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public static class PathsHelper
    {
        public static string DataPath => Path.Combine(Application.persistentDataPath, "Data");
        public static string ModsPath => Path.Combine(Application.persistentDataPath, "Mods");
        public static string StreamingAssetsPath => Application.streamingAssetsPath;
        public static string ModDataJson => "ModData.json";
        public static string ConfigJson => "config.json";
        public static string PlayerDataJson => "playerData.json";
        public static string SettingsDataJson => "settingsData.json";
    }
}