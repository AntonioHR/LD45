using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;

namespace SamuraiGame.Enemy
{
    public class EnemyHurtbox : ObjectTrigger<PlayerController>
    {
        public EnemyController enemy;

        protected override void OnTriggered(PlayerController player)
        {
            if(player.IsDashing())
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

