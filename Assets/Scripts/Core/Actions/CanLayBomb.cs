using Presentation.Game;

namespace Core.Domain.Actions
{
    public class CanLayBomb
    {
        public bool Execute(PlayerStats playerStats)
        {
            return playerStats.Bombs > 0 && !playerStats.BombLayed;
        }
    }
}