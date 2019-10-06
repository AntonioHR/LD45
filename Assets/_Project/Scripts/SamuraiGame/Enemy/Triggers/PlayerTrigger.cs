using System;
using Common.Interactables;
using SamuraiGame.Player;

namespace SamuraiGame.Enemy.Triggers
{
    public class PlayerTrigger : ObjectTrigger<PlayerController>
    {
        public event Action<PlayerController> OnHasPlayer;
        public event Action<PlayerController> OnPlayerLeft;

        protected override void OnTriggered(PlayerController player)
        {
            if(OnHasPlayer!= null)
                OnHasPlayer(player);
        }
        protected override void OnExit(PlayerController player)
        {
            if(OnPlayerLeft!= null)
                OnPlayerLeft(player);
        }
    }
}