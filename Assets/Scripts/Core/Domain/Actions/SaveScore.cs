using Core.Domain.Score;

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
            statsRepository.Put(new Stats.PlayerScore(score));
        }
    }
}