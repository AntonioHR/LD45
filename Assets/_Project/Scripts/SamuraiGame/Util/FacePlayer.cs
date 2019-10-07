using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiGame.Managers;
using SamuraiGame.Enemy;

public class FacePlayer : MonoBehaviour
{
    private EnemyController enemyController;
    public bool isParticle = false;

    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();

    }
    private void LateUpdate()
    {
        if (!isParticle)
        {
            var scale = transform.localScale;
            float abs = Mathf.Abs(scale.x);
            if (enemyController.FacingDirection.x < 0)
                scale.x = -1 * abs;
            else
                scale.x = 1 * abs;
            transform.localScale = scale;
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, -enemyController.FacingDirection);
        } else
        {
            if (enemyController.FacingDirection.x < 0)
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            else
                transform.rotation = Quaternion.identity;

        }
    }
}
