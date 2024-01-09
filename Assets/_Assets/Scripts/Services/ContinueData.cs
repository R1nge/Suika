using System.Collections.Generic;

namespace _Assets.Scripts.Services
{
    public class ContinueData
    {
        public int SongIndex;
        public List<SuikaContinueData> SuikasContinueData;
        public int CurrentSuikaIndex;
        public int NextSuikaIndex;
        public int Score;

        public ContinueData(int songIndex, List<SuikaContinueData> suikasContinueData, int currentSuikaIndex, int nextSuikaIndex, int score)
        {
            SongIndex = songIndex;
            SuikasContinueData = suikasContinueData;
            CurrentSuikaIndex = currentSuikaIndex;
            NextSuikaIndex = nextSuikaIndex;
            Score = score;
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