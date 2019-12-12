using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;

namespace SamuraiGame.Enemy
{
    public class EnemyAttackHitbox : ObjectTrigger<PlayerController>
    {
        public EnemyController enemy;
        public bool isHitbox;

        protected override void OnTriggered(PlayerController player)
        {
            player.OnHitByEnemy(isHitbox, transform);

            if(isHitbox && player.IsDashing())
            {
                enemy.HitParried();
            }
        }

        private void OnEnable()
        {
            Reset();
        }
    }
}

