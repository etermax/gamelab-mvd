using Core.Domain.Score;
using Core.Domain.Stats;

namespace Core.Domain.Actions
{
    public class LoadPreviousScore
    {
        readonly StatsRepository statsRepository;

        public LoadPreviousScore(StatsRepository statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        public PlayerScore Execute()
        {
            return statsRepository.Get();
        }
    }
}