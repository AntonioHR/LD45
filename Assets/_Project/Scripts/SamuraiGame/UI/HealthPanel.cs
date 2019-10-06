using System;
using SamuraiGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SamuraiGame.UI
{
    public class HealthPanel : MonoBehaviour
    {
        public Sprite active;
        public Sprite inactive;

        public Image[] items;
        

        private void Start()
        {
            var player = GameManager.Instance.CurrentScene.Player;
            player.HealthChanged += UpdateTo;
            UpdateTo(player.CurrentHealth);
        }
        private void OnDestroy()
        {
            var player = GameManager.Instance.CurrentScene.Player;
            player.HealthChanged += UpdateTo;
        }

        private void UpdateTo(int health)
        {
            for (int i = 0;  i< items.Length; i++)
            {
                items[i].sprite = health - 1 >= i ? active : inactive;
            }
        }
    }
}