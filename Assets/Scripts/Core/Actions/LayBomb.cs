using Presentation.Game;

namespace Core.Domain.Actions
{
    public class LayBomb
    {
        public void Execute(PlayerStats playerStats)
        {
            playerStats.Bombs--;
            playerStats.BombLayed = true;
        }
    }
}