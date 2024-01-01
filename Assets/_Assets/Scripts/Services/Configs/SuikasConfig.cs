using System;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    [CreateAssetMenu(fileName = "Suikas Config", menuName = "Configs/Suikas")]
    public class SuikasConfig : ScriptableObject
    {
        [SerializeField] private SuikaData[] suikas;
        public Suika GetPrefab(int index) => suikas[index].Prefab;
        public int GetPoints(int index) => suikas[index].Points;
        public bool HasPrefab(int index) => suikas[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Suika Prefab;
            public int Points;

            public SuikaData(Suika prefab, int points)
            {
                Prefab = prefab;
                Points = points;
            }
        }
    }
}