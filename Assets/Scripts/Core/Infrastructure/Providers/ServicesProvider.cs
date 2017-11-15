using Core.Domain.Score;
using Core.Infrastructure.Score.Repository;

namespace Core.Domain.Providers
{
    public static class ServicesProvider
    {
        public static StatsRepository ProvideStateRepository()
        {
            return Provider.GetOrInstanciate<StatsRepository>(() => new StatsPlayerPrefsRepository());
        }
    }
}