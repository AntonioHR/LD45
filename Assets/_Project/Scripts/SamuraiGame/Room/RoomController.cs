using UnityEngine;
using System.Collections;
using SamuraiGame.Events;
using System;
using UnityEngine.SceneManagement;
using SamuraiGame.Managers;

namespace SamuraiGame.Room
{
	public class RoomController : MonoBehaviour
	{
        [SerializeField]
        private string nextSceneName;
        [SerializeField]
        private RoomConfig config;
        [SerializeField]
        private RoomSpawner roomSpawner;
        [SerializeField]
        private bool autoStart = true;
        [SerializeField]
        private string startTrigger;

        public static RoomController Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            TriggerManager.StartListening(EventName.OnTransitionOver, OnGateEnter);

            if (autoStart)
                StartRoom();
            else if (startTrigger.Length > 0)
            {
                TriggerManager.StartListening(startTrigger, StartRoom);
                TriggerManager.StartListening(startTrigger, StopListening);
            }
        }

        private void StopListening()
        {
            TriggerManager.StopListening(startTrigger, StartRoom);
            TriggerManager.StopListening(startTrigger, StopListening);
        }

        private void StartRoom(){
            _ = EnemyManager.Instance.StartRoom(config, roomSpawner);
        }

        private void OnGateEnter()
        {
            SceneManager.LoadScene(nextSceneName);
        }
        private void OnRoomCompleted() {
        }
            
        public void ReloadRoom()
		{
            string sceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(sceneName);
		}
    }
}
