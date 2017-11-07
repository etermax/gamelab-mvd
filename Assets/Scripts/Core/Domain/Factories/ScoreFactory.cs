using Core.Domain.Actions;
using Core.Domain.Score;
using Core.Infrastructure.Score.Repository;

namespace Core.Domain.Factories
{
    public static partial class ActionsFactory
    {
        public static SaveScore ProvideSaveScore()
        {
            return GetOrInstanciate<SaveScore>(() => new SaveScore(ProvideStateRepository()));
        }

        public static LoadPreviousScore ProvideLoadScoreFromRepository()
        {
            return GetOrInstanciate<LoadPreviousScore>(
                () => new LoadPreviousScore(ProvideStateRepository()));
        }

        private static StatsRepository ProvideStateRepository()
        {
            return GetOrInstanciate<StatsRepository>(() => new PlayerPrefsRepository());
        }

        public static VerifiesHighScoreBeated ProvideVerifiesHighScoreBeated()
        {
            return GetOrInstanciate<VerifiesHighScoreBeated>(
                () => new VerifiesHighScoreBeated(ProvideStateRepository()));
        }
    }
}