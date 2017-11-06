namespace Core.Domain.Stats
{
    public class Stats
    {
        public const string ScoreKey = "Score";
        public int Score { get; private set; }

        public Stats(int score)
        {
            Score = score;
        }
    }
}