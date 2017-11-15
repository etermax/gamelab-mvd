using Core.Domain.Actions;
using Core.Domain.Score;
using Core.Infrastructure.Score.Repository;

namespace Core.Domain.Providers
{
    public static class ActionsProvider
    {
        public static SaveScore ProvideSaveScore()
        {
            return Provider.GetOrInstanciate<SaveScore>(() => new SaveScore(ServicesProvider.ProvideStateRepository()));
        }

        public static LoadPreviousScore ProvideLoadScoreFromRepository()
        {
            return Provider.GetOrInstanciate<LoadPreviousScore>(
                () => new LoadPreviousScore(ServicesProvider.ProvideStateRepository()));
        }

        public static VerifiesHighScoreBeated ProvideVerifiesHighScoreBeated()
        {
            return Provider.GetOrInstanciate<VerifiesHighScoreBeated>(
                () => new VerifiesHighScoreBeated(ServicesProvider.ProvideStateRepository()));
        }

        public static HurtEnemy ProvideHurtEnemy()
        {
            return new HurtEnemy();
        }

        public static CanLayBomb ProvideCanLayBomb()
        {
            return new CanLayBomb();
        }

        public static LayBomb ProvideLayBomb()
        {
            return new LayBomb();
        }
    }
}