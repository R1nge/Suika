using System.IO;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace _Assets.Scripts.Services.Datas.Mods
{
    public class ModDataLoaderJson : IModDataLoader
    {
        private ModData _modData = new(0);
        public ModData ModData => _modData;

        public async UniTask Load()
        {
            var dataFolderInfo = new DirectoryInfo(PathsHelper.DataPath);

            if (!dataFolderInfo.Exists)
            {
                dataFolderInfo.Create();
                return;
            }
            
            foreach (var fileInfo in dataFolderInfo.GetFiles("modData.json"))
            {
                var reader = new StreamReader(fileInfo.FullName);
                var json = await reader.ReadToEndAsync();
                _modData = JsonConvert.DeserializeObject<ModData>(json);
                reader.Close();
                reader.Dispose();
            }
        }

        public void Save()
        {
            var path = Path.Combine(PathsHelper.DataPath, "modData.json");
            var json = $"{_modData.GetHashCode()}\n" + JsonConvert.SerializeObject(_modData);
            File.WriteAllText(path, json);
        }
    }
}