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
        var scale = transform.localScale;
        float abs = Mathf.Abs(scale.x);
        if (enemyController.FacingDirection.x < 0)
            scale.x = -1 * abs;
        else
            scale.x = 1 * abs;
        transform.localScale = scale;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, -enemyController.FacingDirection);
    }
}
