namespace Game.Events
{
    public interface GameEvent
    {
        GameEventType GetType();
    }

    public enum GameEventType
    {
        RocketImpactsEnemy
    }
}