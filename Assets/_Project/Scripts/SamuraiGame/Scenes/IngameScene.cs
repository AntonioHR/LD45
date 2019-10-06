using SamuraiGame.Managers;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Scenes
{
    public class IngameScene : MonoBehaviour
    {
        [SerializeField]
        PlayerController player;

        public PlayerController Player{ get => player; }
        public void Start()
        {
            GameManager.Instance.OnEnteredScene(this);
        }
    }
}