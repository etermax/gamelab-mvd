using Core.Domain.Score;
using Core.Domain.Stats;

namespace Core.Domain.Actions
{
    public class SaveScore
    {
        readonly StatsRepository statsRepository;

        public SaveScore(StatsRepository statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        public void Execute(int score)
        {
            if (statsRepository.Get().Score < score)
            {
                statsRepository.Put(new PlayerScore(score));
            }
        }
    }
}