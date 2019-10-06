using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace SamuraiGame.Enemy.States {

    public class EnemyStaggerState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        protected override void Begin()
        {
            var invi = new Color(1, 1, 1, 0);
            Enemy.Rigidbody.velocity = -Enemy.TargetDirection * .5f;

            var seq = DOTween.Sequence();
            seq.Append(Enemy.sprite.DOColor(invi, .05f));
            seq.Append(Enemy.sprite.DOColor(Color.white, .05f));
            seq.AppendInterval(.05f);
            seq.SetLoops(4);
            
            seq.OnComplete(Destroy);
        }

        private void Destroy()
        {
            GameObject.Destroy(Enemy.gameObject);
        }
    }
}
