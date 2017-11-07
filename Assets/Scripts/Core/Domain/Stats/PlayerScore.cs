namespace Core.Domain.Stats
{
    public class PlayerScore
    {
        public const string ScoreKey = "Score";
        public int InternalScore { get; private set; }

        public PlayerScore(int score)
        {
            InternalScore = score;
        }
    }
}