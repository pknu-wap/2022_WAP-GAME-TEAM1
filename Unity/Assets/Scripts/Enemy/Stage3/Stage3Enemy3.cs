using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Enemy3 : Enemy
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] Transform firePos;
    [SerializeField] Vector2 BoxArea;

    [SerializeField] GameObject bullet;

    [SerializeField] float shotCoolTime;
    private float curShotCoolTime;

    [SerializeField] private bool moveRight;
    private bool playerSpotted;
    private State _state;
   

    enum State
    {
        Move,
        Attack
    }

    void OnEnable()
    {
        _state = State.Move;
        curShotCoolTime = 2;
        StartCoroutine(Enemy3Co());
    }

    
    IEnumerator Enemy3Co()
    {
       while (true)
       {
            switch (_state)
            {
                case State.Move:

                    if (moveRight)
                    {
                        enemyRB.velocity = new Vector2(moveSpeed, 0f);
                        enemyTransform.localScale = new Vector2(-1.2f, enemyTransform.localScale.y);

                        if (enemyTransform.position.x >= rightPos.position.x)
                        {
                            moveRight = false;
                        }
                    }
                    else
                    {
                        enemyRB.velocity = new Vector2(-moveSpeed, 0f);
                        enemyTransform.localScale = new Vector2(1.2f, enemyTransform.localScale.y);

                        if (enemyTransform.position.x <= leftPos.position.x)
                        {
                            moveRight = true;
                        }
                    }

                    
                    playerSpotted = Physics2D.OverlapBox(enemyTransform.position, BoxArea, 0, playerLayer);
                    if (curShotCoolTime >= shotCoolTime)
                    {          
                        if (playerSpotted)
                        {
                            if (enemyTransform.position.x < PlayerController.instance.transform.position.x)
                                enemyTransform.localScale = new Vector2(-1.2f, enemyTransform.localScale.y);
                            else
                                enemyTransform.localScale = new Vector2(1.2f, enemyTransform.localScale.y);

                            _state = State.Attack;
                        }
                        curShotCoolTime = 0;
                    }
                    curShotCoolTime += Time.deltaTime;                 
                    break;

                case State.Attack:
                    enemyRB.velocity = new Vector2(0f, 0f);
                    var _bullet = Instantiate(bullet, firePos.position, Quaternion.identity);
                    _bullet.transform.localScale = enemyTransform.localScale;
                    yield return new WaitForSeconds(1f);
                    _state = State.Move;
                    break;
            }
            yield return null;
       }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(enemyTransform.position, BoxArea);
    }
}
