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
        private GameObject gateObject;
        [SerializeField]
        private string nextSceneName;
        [SerializeField]
        private RoomConfig config;
        [SerializeField]
        private RoomSpawner roomSpawner;

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
            TriggerManager.StartListening(EventName.RoomCompleted, OnRoomCompleted);
            TriggerManager.StartListening(EventName.OnGateEnter, OnGateEnter);

            gateObject.SetActive(false);

            StartRoom();
        }

        private void StartRoom(){
            _ = EnemyManager.Instance.StartRoom(config, roomSpawner);
        }

        private void OnGateEnter()
        {
            SceneManager.LoadScene(nextSceneName);
            gateObject.SetActive(false);
        }

        private void OnRoomCompleted() {
            EnableGate();
        }
            
        private void EnableGate() {
            gateObject.SetActive(true);
        }

        public void ReloadRoom()
		{
            string sceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(sceneName);
		}
    }
}
