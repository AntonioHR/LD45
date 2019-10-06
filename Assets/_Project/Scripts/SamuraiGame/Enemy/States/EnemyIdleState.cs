using UnityEngine;
using System.Collections;
using SamuraiGame.Player;
using DG.Tweening;
using System;

namespace SamuraiGame.Enemy.States {

    public class EnemyIdleState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        public override bool TryAttack()
        {
            ExitTo(new EnemyAttackState());
            return true;
        }

        protected override void Begin()
        {

        }
        public override  void OnPlayerIsInAggroRange(PlayerController player)
        {
            Enemy.target = player;
            player.Died += Enemy.OnPlayerDead;

            FlickerWarning();

            ExitTo(new PursueState());

        }

        private async void FlickerWarning()
        {
            Enemy.emoteSprite.color = Color.white;
            await Wait.For(3);
            if(Enemy != null)
                Enemy.emoteSprite.color = new Color(1,1,1, 0);
        }
    }
}
