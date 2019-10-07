using UnityEngine;
using System.Collections;
using SamuraiGame.Events;
using UnityEngine.SceneManagement;

namespace SamuraiGame.Room
{
    public class BossTransitionHelper : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            TriggerManager.StartListening(EventName.OnBossKilled, OnBossKilled);
            TriggerManager.StartListening(EventName.OnTransitionOver, ChangeToCutscene);
        }

        private void ChangeToCutscene()
        {
            SceneManager.LoadScene("Ending");
        }

        private void OnDestroy()
        {
            TriggerManager.StopListening(EventName.OnBossKilled, OnBossKilled);
            TriggerManager.StopListening(EventName.OnTransitionOver, ChangeToCutscene);
        }

        async void OnBossKilled()
        {
            await Wait.For(GameConstants.BOSS_KILLED_WAIT_TIME);

            TriggerManager.Trigger(EventName.OnGateEnter);
        }
    }
}
