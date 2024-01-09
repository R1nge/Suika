using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services
{
    public struct ContinueData
    {
        public int SongIndex;
        public List<SuikaContinueData> SuikasContinueData;
        public int CurrentSuikaIndex;
        public int NextSuikaIndex;

        public ContinueData(int songIndex, List<SuikaContinueData> suikasContinueData, int currentSuikaIndex, int nextSuikaIndex)
        {
            SongIndex = songIndex;
            SuikasContinueData = suikasContinueData;
            CurrentSuikaIndex = currentSuikaIndex;
            NextSuikaIndex = nextSuikaIndex;
        }
        
        public struct SuikaContinueData
        {
            public int Index;
            public float PositionX, PositionY;

            public SuikaContinueData(int index, float positionX, float positionY)
            {
                Index = index;
                PositionX = positionX;
                PositionY = positionY;
            }
        }
    }
}