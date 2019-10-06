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
            GameManager.Instance.OnEnteredScene(this);
        }
    }
}