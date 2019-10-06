using Common.Interactables;
using DG.Tweening;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Pickups
{
    public class HealthPickup : ObjectTrigger<Player.PlayerController>
    {

        public void Start()
        {
            transform.DOPunchPosition(Vector3.up * .7f, .75f, elasticity: 0);


            var beat = DOTween.Sequence();
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.Append(transform.DOPunchScale(Vector3.one * .5f, .3f, 0, 0));
            beat.AppendInterval(2);
            beat.SetLoops(-1);
            beat.SetDelay(1);
        }
        private void OnDestroy()
        {
            DOTween.Kill(transform);
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