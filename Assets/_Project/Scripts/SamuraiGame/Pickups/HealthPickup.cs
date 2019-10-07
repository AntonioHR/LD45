using Common.Interactables;
using DG.Tweening;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Pickups
{
    public class HealthPickup : ObjectTrigger<Player.PlayerController>
    {
        private Sequence beat;
        private Tween bounce;

        public void Start()
        {
            bounce = transform.DOPunchPosition(Vector3.up * .7f, .75f, elasticity: 0);


            beat = DOTween.Sequence();
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.AppendInterval(2);
            beat.SetLoops(-1);
            beat.SetDelay(1);
        }
        private void OnDestroy()
        {
            beat.Kill();
            bounce.Kill();
        }
        protected override void OnTriggered(PlayerController player)
        {
            if (player.CanHeal)
            {
                player.Heal();
                Destroy(gameObject);
            }
        }
    }
}