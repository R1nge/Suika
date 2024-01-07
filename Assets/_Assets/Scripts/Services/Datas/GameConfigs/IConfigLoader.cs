using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public interface IConfigLoader
    {
        public GameConfig CurrentConfig { get; }
        public List<GameConfig> AllConfigs { get; }
        public UniTask LoadDefaultConfig();
        public void LoadAllConfigs();
        public void SetCurrentConfig(int index);
        public bool IsDefault { get; }
    }
}