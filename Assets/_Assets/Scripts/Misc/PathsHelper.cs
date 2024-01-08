using System.IO;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public static class PathsHelper
    {
        public static string DataPath => Path.Combine(Application.persistentDataPath, "Data");
        public static string ModsPath => Path.Combine(DataPath, "Mods");
        public static string StreamingAssetsPath => Application.streamingAssetsPath;
    }
}