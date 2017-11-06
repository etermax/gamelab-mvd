namespace Core.Domain.Score
{
    public interface StatsRepository
    {
        void Put(Stats.Stats stats);
        Stats.Stats Get();
    }
}