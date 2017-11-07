namespace Core.Domain.Stats
{
    public class PlayerScore
    {
        public const string ScoreKey = "Score";
        public int Score { get; private set; }

        public PlayerScore(int score)
        {
            Score = score;
        }
    }
}