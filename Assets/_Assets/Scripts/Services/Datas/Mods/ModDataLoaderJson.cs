using System.IO;
using System.Text.RegularExpressions;
using _Assets.Scripts.Misc;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace _Assets.Scripts.Services.Datas.Mods
{
    public class ModDataLoaderJson : IModDataLoader
    {
        private ModData _modData = new(0);
        //TODO: encrypt hash
        private int _hash;
        public ModData ModData => _modData;
        public bool IsTheSame => _hash == _modData.GetHashCode();

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
                _hash = int.Parse(json.Split('\n')[0]);
                
                var firstLine = json.Substring(0, json.IndexOf('\n') + 1);
                json = Regex.Replace(json, firstLine, "");

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