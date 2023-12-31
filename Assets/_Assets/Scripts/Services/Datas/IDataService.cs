using System.Collections.Generic;

namespace _Assets.Scripts.Services.Datas
{
    public interface IDataService
    {
        List<GameData> GameDatas { get; }
        void SaveData();
        void LoadData();
    }
}