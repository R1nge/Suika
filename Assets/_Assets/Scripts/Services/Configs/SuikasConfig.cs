using System;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    [CreateAssetMenu(fileName = "Suikas Config", menuName = "Configs/Suikas")]
    public class SuikasConfig : ScriptableObject
    {
        [SerializeField] private SuikaData[] suikas;

        private void OnValidate()
        {
            for (int i = 0; i < suikas.Length; i++)
            {
                if (suikas[i].DropChance <= 0)
                {
                    Debug.LogError($"Drop chance can't be less or equal to 0. Suika index {i} has {suikas[i].DropChance}");
                }

                if (suikas[i].DropChance >= 1)
                {
                    Debug.LogError($"Drop chance can't be more or equal to 1. Suika index {i} has {suikas[i].DropChance}");
                }
            }
        }

        public Sprite GetIcon(int index) => suikas[index].Icon;
        public Suika GetPrefab(int index) => suikas[index].Prefab;
        public int GetPoints(int index) => suikas[index].Points;
        public float GetDropChance(int index) => suikas[index].DropChance;
        public bool HasPrefab(int index) => suikas[index].Prefab != null;

        [Serializable]
        public struct SuikaData
        {
            public Sprite Icon;
            public Suika Prefab;
            public int Points;
            public float DropChance;

            public SuikaData(Sprite icon, Suika prefab, int points, float dropChance)
            {
                Icon = icon;
                Prefab = prefab;
                Points = points;
                DropChance = dropChance;
            }
        }
    }
}