using Core.Domain.Score;

namespace Core.Domain.Actions
{
    public class VerifiesHighScoreBeated
    {
        readonly StatsRepository statsRepository;

        public VerifiesHighScoreBeated(StatsRepository statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        public bool Execute(int score)
        {
            return score > statsRepository.Get().Score;
        }
    }
}