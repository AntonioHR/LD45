using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiGame.Managers;
using SamuraiGame.Enemy;

public class FacePlayer : MonoBehaviour
{
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();

    }
    private void Update()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, -enemyController.FacingDirection);
    }
}
