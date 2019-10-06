﻿using UnityEngine;
using System.Collections;
using SamuraiGame.Managers;
using SamuraiGame.Player;

namespace SamuraiGame.Room
{
    public class CheckpointController : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            PlayerController player = GameManager.Instance.CurrentScene.Player;
			player.Died += OnPlayerDeath;
        }


        void OnPlayerDeath()
        {
            //play death cutscene
            RoomController.Instance.ReloadRoom();
        }
    }
}