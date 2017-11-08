using Core.Domain.Stats;

namespace Core.Domain.Score
{
    public interface StatsRepository
    {
        void Put(PlayerScore playerScore);
        PlayerScore Get();
    }
}