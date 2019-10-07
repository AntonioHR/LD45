using UnityEngine;
using System.Collections;
using DG.Tweening;
using SamuraiGame;
namespace SamuraiGame.Enemy
{
    public class BossBehaviour : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            GetComponent<EnemyController>().disableInteractions = true;

            var seq = DOTween.Sequence();
		    seq.Join(transform.DOMoveY(GameConstants.BOSS_WALKING_Y_DISTANCE, GameConstants.BOSS_WALKING_TIME).SetRelative());
		    seq.OnComplete(StartBehaviour);
	    }

        async void StartBehaviour()
        {
            await Wait.For(GameConstants.BOSS_WAIT_TIME - GameConstants.BOSS_WALKING_TIME);

            GetComponent<EnemyController>().disableInteractions = false;
        }

    }
}

