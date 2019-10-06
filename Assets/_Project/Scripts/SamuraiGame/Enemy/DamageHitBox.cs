using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;

namespace SamuraiGame.Enemy
{
    public class DamageHitBox : ObjectTrigger<PlayerController>
    {
        public EnemyController enemy;
        public bool isDashable;

        protected override void OnTriggered(PlayerController player)
        {
            player.OnHitByEnemy(isDashable, transform);

            if(isDashable && player.IsDashing())
            {
                
            }
        }
    }
}

