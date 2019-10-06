using System;
using SamuraiGame.Scenes;
using UnityEngine;

namespace SamuraiGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        public IngameScene CurrentScene{ get; private set; }

        public static GameManager Instance { get; private set; }

        public void OnEnteredScene(IngameScene ingameScene)
        {
            this.CurrentScene = ingameScene;
        }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else
            {
                Destroy(this);
            }
        }



    }
}