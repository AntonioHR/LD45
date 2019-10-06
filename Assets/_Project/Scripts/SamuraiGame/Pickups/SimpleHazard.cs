using Common.Interactables;
using SamuraiGame.Player;

namespace SamuraiGame.Pickups
{
    public class SimpleHazard : ObjectTrigger<Player.PlayerController>
    {
        protected override void OnTriggered(PlayerController player)
        {
            player.OnHit(transform);
        }
    }
}