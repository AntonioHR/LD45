

using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;

namespace SamuraiGame.Sword
{
    public class SwordController : MonoBehaviour
    {
        const float TIME_FALL = 0.5f;
        const float TIME_FALL_RANDOM = 0.15f;
        const float DISTANCE_FALL_Y = 1.7f;
        const float DISTANCE_FALL_Y_RANDOM = 0.3f;
        const float DISTANCE_FALL_X = 2f;
        const float DISTANCE_FALL_X_RANDOM = 0.5f;
        [SerializeField]
        private Sprite swordOnGround;

        private SpriteRenderer sprite;

        private Animator swordAnimator;

        private bool hasFinished;

        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            swordAnimator = GetComponent<Animator>();

            StartAnimation();
        }

        public void StartAnimation () {
            float distanceUp = DISTANCE_FALL_Y + UnityEngine.Random.Range(-DISTANCE_FALL_Y_RANDOM, DISTANCE_FALL_Y_RANDOM);
            float distanceDown = -DISTANCE_FALL_Y + UnityEngine.Random.Range(-DISTANCE_FALL_Y_RANDOM, DISTANCE_FALL_Y_RANDOM);

            float time = TIME_FALL + UnityEngine.Random.Range(-TIME_FALL_RANDOM, TIME_FALL_RANDOM);

            int direction = UnityEngine.Random.Range(0, 2) * 2 - 1;
            float distanceX = direction * DISTANCE_FALL_X + UnityEngine.Random.Range(-DISTANCE_FALL_X_RANDOM, DISTANCE_FALL_X_RANDOM);

            var seq = DOTween.Sequence();
		    seq
            .Append(transform.DOMoveY(distanceUp, time).SetRelative().SetEase(Ease.OutQuad))
            .Append(transform.DOMoveY(distanceDown, time).SetRelative().SetEase(Ease.InQuad));

            var xSeq = DOTween.Sequence();
            xSeq.Append(transform.DOMoveX(distanceX, 2*time).SetRelative().SetEase(Ease.Linear));
            xSeq.Join(seq);
		    seq.OnComplete(FinishAnimation);
        }

        private void FinishAnimation() {
            swordAnimator.SetTrigger("FinishAnimation");
            hasFinished = true;
        }

        void LateUpdate()
        {
            if(hasFinished) {
                sprite.sprite = swordOnGround;
            }
        }
    }
}