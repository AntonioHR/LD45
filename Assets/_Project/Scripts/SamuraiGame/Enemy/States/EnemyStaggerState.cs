using UnityEngine;
using System.Collections;
using DG.Tweening;
using SamuraiGame.Player;
using SamuraiGame.Managers;
using System.Threading.Tasks;
using SamuraiGame.Events;
using System;

namespace SamuraiGame.Enemy.States {

    public class EnemyStaggerState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        protected override void Begin()
        {
            RandomSwordAnimation();

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

            StartDropWeaponAnimation();
            //seq.OnComplete(Destroy);
        }

        private void RandomSwordAnimation()
        {
            if(UnityEngine.Random.Range(0f, 1f) < 0.5f) {
                SwordManager.Instance.AnimateSword(Enemy.transform.position);
            } else {
                Enemy.swordParticles.Play();
            }
        }

        private void Destroy()
        {
            GameObject.Destroy(Enemy.gameObject);
        }
        public override void OnPlayerDead() { }

        private void StartDropWeaponAnimation()
        {
            string animationId = GameConstants.ENEMY_ANIMATION_DISARM;
            Enemy.animationPlayable.PlayOnce(animationId, DoFade);
        }

        private async void DoFade()
        {
            Vector3 scale = Enemy.transform.localScale;

            if(!Enemy.isBoss) {
                string animationId = GameConstants.ENEMY_ANIMATION_ESCAPING;
                Enemy.animationPlayable.PlayLooped(animationId, () => { });

                scale.x = -scale.x;
                Enemy.transform.localScale = scale;

                PlayerController player = GameManager.Instance.CurrentScene.Player;

                Vector2 direction = Enemy.transform.position - player.transform.position;

                Enemy.Rigidbody.velocity = direction * 3;

                var invi = new Color(1, 1, 1, 0);

                await Wait.For(0.7f);

                var seq = DOTween.Sequence();
                seq.Append(Enemy.sprite.DOColor(invi, .1f));
                seq.Append(Enemy.sprite.DOColor(Color.white, .1f));
                seq.AppendInterval(.1f);
                seq.SetLoops(4);

                seq.OnComplete(Destroy);
            } else {
                string animationId = GameConstants.ENEMY_ANIMATION_ESCAPING;
                Enemy.animationPlayable.PlayOnce(animationId, () => { });

                TriggerManager.Trigger(EventName.OnBossKilled);
            }
        }
    }
}
