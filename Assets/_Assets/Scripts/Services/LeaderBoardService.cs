using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services
{
    public class LeaderBoardService
    {
        private readonly IDataService _dataService;

        private LeaderBoardService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public int GetScore(int index)
        {
            if (index > _dataService.GameDatas.Count - 1)
            {
                return 0;
            }

            return _dataService.GameDatas[index].Score;
        }
    }
}