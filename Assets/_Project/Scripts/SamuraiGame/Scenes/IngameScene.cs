using SamuraiGame.Events;
using SamuraiGame.Managers;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Scenes
{
    public class IngameScene : MonoBehaviour
    {
        [SerializeField]
        PlayerController player;
        [SerializeField]
        private bool isFirstScene = false;

        public PlayerController Player{ get => player; }
        public bool IsFirstScene { get => isFirstScene; }

        public void Start()
        {
            if(IsFirstScene)
            {
                TriggerManager.Trigger(EventName.OnFirstScene);
            }
            GameManager.Instance.OnEnteredScene(this);
        }
    }
}