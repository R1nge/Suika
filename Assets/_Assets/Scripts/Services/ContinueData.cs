using System.Collections.Generic;
using _Assets.Scripts.Services.GameModes;

namespace _Assets.Scripts.Services
{
    public class ContinueData
    {
        public int SongIndex;
        public List<SuikaContinueData> SuikasContinueData;
        public int CurrentSuikaIndex;
        public int NextSuikaIndex;
        public int Score;
        public float TimeRushTime;
        public GameModeService.GameMode GameMode;

        public ContinueData(int songIndex, List<SuikaContinueData> suikasContinueData, int currentSuikaIndex, int nextSuikaIndex, int score, float timeRushTime, GameModeService.GameMode gameMode)
        {
            SongIndex = songIndex;
            SuikasContinueData = suikasContinueData;
            CurrentSuikaIndex = currentSuikaIndex;
            NextSuikaIndex = nextSuikaIndex;
            Score = score;
            TimeRushTime = timeRushTime;
            GameMode = gameMode;
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