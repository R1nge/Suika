namespace _Assets.Scripts.Services.StateMachine
{
    public enum GameStateType : byte
    {
        None = 0,
        LoadSavedData = 1,
        Game = 2,
        GameOver = 3,
        SaveData = 4,
        ResetAndRetry = 5,
        ResetAndMainMenu = 6
    }
}