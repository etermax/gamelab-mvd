using Core.Domain.Score;
using Core.Domain.Stats;
using UnityEngine;

namespace Core.Infrastructure.Score.Repository
{
    public class PlayerPrefsRepository : StatsRepository
    {
        public void Put(PlayerScore score)
        {
            PlayerPrefs.SetInt(PlayerScore.ScoreKey, score.Score);
        }

        public PlayerScore Get()
        {
            return new PlayerScore(PlayerPrefs.GetInt(PlayerScore.ScoreKey));
        }
    }
}