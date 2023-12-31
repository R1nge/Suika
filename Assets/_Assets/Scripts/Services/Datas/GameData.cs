using System;

[Serializable]
public struct GameData : IComparable<GameData>
{
    public int Score;

    public GameData(int score)
    {
        Score = score;
    }

    public int CompareTo(GameData other)
    {
        return Score.CompareTo(other.Score);
    }
}