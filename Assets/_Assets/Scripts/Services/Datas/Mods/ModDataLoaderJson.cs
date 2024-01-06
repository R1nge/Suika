using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Services.Datas.Mods
{
    public class ModDataLoaderJson : IModDataLoader
    {
        private ModData _modData = new(0);
        public ModData ModData => _modData;
        private readonly string _path = Path.Combine(Application.persistentDataPath, "Data");

        public async UniTask Load()
        {
            var modsFoldersDirectoryInfo = new DirectoryInfo(_path);

            if (!modsFoldersDirectoryInfo.Exists)
            {
                modsFoldersDirectoryInfo.Create();
                return;
            }
            
            foreach (var directoryInfo in modsFoldersDirectoryInfo.GetDirectories())
            {
                foreach (var fileInfo in directoryInfo.GetFiles("modData.json"))
                {
                    StreamReader reader = new StreamReader(fileInfo.FullName);
                    var json = await reader.ReadToEndAsync();
                    _modData = JsonConvert.DeserializeObject<ModData>(json);
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        public void Save()
        {
            var path = Path.Combine(_path, "modData.json");
            var json = JsonConvert.SerializeObject(_modData);
            File.WriteAllText(path, json);
        }
    }
}