namespace _Assets.Scripts.Services.Datas.GameConfigs
{
    public interface IConfigLoader
    {
        public GameConfig GameConfig { get; }
        public GameConfig LoadConfig();
    }
}