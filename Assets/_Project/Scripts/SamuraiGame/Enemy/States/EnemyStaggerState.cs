using UnityEngine;
using System.Collections;
using DG.Tweening;
using SamuraiGame.Player;
using SamuraiGame.Managers;
using System.Threading.Tasks;

namespace SamuraiGame.Enemy.States {

    public class EnemyStaggerState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        protected override void Begin()
        {
            Collider2D collider = Enemy.GetComponent<Collider2D>();
            collider.enabled = false;

            RemovePlayerListener();
            //var invi = new Color(1, 1, 1, 0);
            Enemy.Rigidbody.velocity = -Enemy.TargetDirection * .5f;
            Enemy.Rigidbody.velocity = Vector2.zero;

            //var seq = DOTween.Sequence();
            //seq.Append(Enemy.sprite.DOColor(invi, .05f));
            //seq.Append(Enemy.sprite.DOColor(Color.white, .05f));
            //seq.AppendInterval(.05f);
            //seq.SetLoops(4);

            TrySpawnPickup();

            _ = StartDropWeaponAnimation();
            //seq.OnComplete(Destroy);
        }

        private void Destroy()
        {
            GameObject.Destroy(Enemy.gameObject);
        }
        public override void OnPlayerDead() { }

        private async Task StartDropWeaponAnimation()
        {
            string animationId = GameConstants.ENEMY_ANIMATION_DISARM;
            Enemy.animationPlayable.PlayOnce(animationId, DoFade);
        }

        private void DoFade()
        {
            Vector3 scale = Enemy.transform.localScale;
            scale.x = -scale.x;
            Enemy.transform.localScale = scale;

            string animationId = GameConstants.ENEMY_ANIMATION_ESCAPING;
            Enemy.animationPlayable.PlayLooped(animationId, () => { });

            PlayerController player = GameManager.Instance.CurrentScene.Player;

            Vector2 direction = Enemy.transform.position - player.transform.position;

            Enemy.Rigidbody.velocity = direction * 3;

            var invi = new Color(1, 1, 1, 0);

            var seq = DOTween.Sequence();
            seq.Append(Enemy.sprite.DOColor(invi, .05f));
            seq.Append(Enemy.sprite.DOColor(Color.white, .05f));
            seq.AppendInterval(.05f);
            seq.SetLoops(4);

            seq.OnComplete(Destroy);
        }
    }
}
