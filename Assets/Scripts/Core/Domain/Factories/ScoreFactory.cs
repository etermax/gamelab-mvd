using Core.Domain.Actions;
using Core.Domain.Score;
using Core.Infrastructure.Score.Repository;

namespace Core.Domain.Factories
{
    public partial class ActionsFactory
    {
        public static SaveScore ProvideSaveScore()
        {
            return GetOrInstanciate<SaveScore>(() => new SaveScore(ProvideStateRepository()));
        }

        private static StatsRepository ProvideStateRepository()
        {
            return GetOrInstanciate<StatsRepository>(() => new PlayerPrefsRepository());
        }
    }
}