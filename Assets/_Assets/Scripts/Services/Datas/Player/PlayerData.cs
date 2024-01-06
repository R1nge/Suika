using System;

[Serializable]
public struct PlayerData : IComparable<PlayerData>
{
    public int Score;

    public PlayerData(int score)
    {
        Score = score;
    }

    public int CompareTo(PlayerData other)
    {
        return Score.CompareTo(other.Score);
    }
}