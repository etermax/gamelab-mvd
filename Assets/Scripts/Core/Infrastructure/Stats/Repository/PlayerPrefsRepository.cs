using Core.Domain.Score;
using Core.Domain.Stats;
using UnityEngine;

namespace Core.Infrastructure.Score.Repository
{
    public class PlayerPreferencesRepository : StatsRepository
    {
        public void Put(Stats stats)
        {
            PlayerPrefs.SetInt(Stats.ScoreKey, stats.Score);
        }

        public Stats Get()
        {
            return new Stats(PlayerPrefs.GetInt(Stats.ScoreKey));
        }
    }
}