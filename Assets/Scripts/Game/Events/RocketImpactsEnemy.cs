namespace Game.Events
{
    public class RocketImpactsEnemy : GameEvent
    {
        public IEnemy Enemy { get; set; }
        public IRocket Rocket { get; set; }
        
        public GameEventType GetType()
        {
            return GameEventType.RocketImpactsEnemy;
        }
    }
}