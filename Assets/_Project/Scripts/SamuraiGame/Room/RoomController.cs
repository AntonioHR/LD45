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

        private void Start()
        {
            TriggerManager.StartListening(EventName.RoomCompleted, OnRoomCompleted);
            TriggerManager.StartListening(EventName.OnGateEnter, OnGateEnter);

            gateObject.SetActive(false);

            StartRoom();
        }

        private void StartRoom(){
            EnemyManager.Instance.StartRoom(config);
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
        
    }
}
