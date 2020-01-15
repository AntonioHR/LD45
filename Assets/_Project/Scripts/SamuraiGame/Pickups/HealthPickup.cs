using System.Threading.Tasks;
using Common.Interactables;
using DG.Tweening;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Pickups
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HealthPickup : ObjectTrigger<Player.PlayerController>
    {
        private SpriteRenderer spriteRenderer;
        private Sequence beat;
        private Tween bounce;
        private Sequence blink;
        private Sequence disappear;

        public void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            bounce = transform.DOPunchPosition(Vector3.up * .7f, .75f, elasticity: 0);


            beat = DOTween.Sequence();
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.AppendInterval(2);
            beat.SetLoops(-1);
            beat.SetDelay(1);

            BlinkToDisappear();
        }
        private void OnDestroy()
        {
            beat.Kill();
            bounce.Kill();
            disappear.Kill();
            blink.Kill();
        }
        protected override void OnTriggered(PlayerController player)
        {
            if (player.CanHeal)
            {
                player.Heal();
                Destroy(gameObject);
            }
        }

        private void BlinkToDisappear() {
            disappear = DOTween.Sequence();
            disappear.AppendInterval(GameConstants.HEALTH_START_BLINKING_TIME);
            disappear.AppendCallback(StartBlink);

            disappear.AppendInterval(GameConstants.HEALTH_BLINK_TO_DISAPPEAR_TIME);
            disappear.Append(spriteRenderer.DOFade(0, GameConstants.HEALTH_BLINK_TIME));
            disappear.AppendCallback(() => {
                if(gameObject != null) {
                    Destroy(gameObject);
                }
            });
        }

        private void StartBlink() {
                if(gameObject != null) {
                    blink = DOTween.Sequence();
                    blink.Append(spriteRenderer.DOFade(0, GameConstants.HEALTH_BLINK_TIME));
                    blink.Append(spriteRenderer.DOFade(1, GameConstants.HEALTH_BLINK_TIME));
                    blink.SetLoops(-1);
                }
        }
    }
}