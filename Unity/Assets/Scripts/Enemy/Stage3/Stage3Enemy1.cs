using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Enemy1 : Enemy
{
    private bool isFront;
    private bool isEnemySpotted;
    public Vector2 BoxArea;

    void OnEnable()
    {
        enemyRB.velocity = new Vector2(-moveSpeed, 0f);
        enemyTransform.localScale = new Vector2(1f, 1f);
        isEnemySpotted = false;
    }

    void Update()
    {
        isFront = Physics2D.OverlapCircle(frontPos.position, 0.1f, groundLayer);
        isEnemySpotted = Physics2D.OverlapBox(centerPos.position, BoxArea, 0, playerLayer);

        if (!isEnemySpotted)
        {
            if (isFront)
            {
                if (enemyRB.velocity.x < 0)
                {
                    enemyRB.velocity = new Vector2(moveSpeed, 0f);
                    enemyTransform.localScale = new Vector2(-1f, 1f);
                }
                else
                {
                    enemyRB.velocity = new Vector2(-moveSpeed, 0f);
                    enemyTransform.localScale = new Vector2(1f, 1f);
                }

            }
        }
        else
        {
            if (centerPos.position.x < PlayerController.instance.transform.position.x)
            {
                enemyRB.velocity = new Vector2(moveSpeed, 0f);
                enemyTransform.localScale = new Vector2(-1f, 1f);
            }
            else
            {
                enemyRB.velocity = new Vector2(-moveSpeed, 0f);
                enemyTransform.localScale = new Vector2(1f, 1f);
            }
        }
    }
}
