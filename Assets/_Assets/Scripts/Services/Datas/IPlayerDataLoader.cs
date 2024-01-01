using System.Collections.Generic;

namespace _Assets.Scripts.Services.Datas
{
    public interface IPlayerDataLoader
    {
        List<GameData> GameDatas { get; }
        void SaveData();
        void LoadData();
    }
}