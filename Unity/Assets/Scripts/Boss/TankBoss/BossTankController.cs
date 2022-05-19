using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates
    {
        shooting,
        moving,
        die
    };

    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;
    public SpriteRenderer tankSR;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public float speedUp;
    private bool moveRight;

    [Header("Shooting")]
    public BossTankBullet bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    public float shotSpeedUp;
    private float shotCounter;
    

    private List<BossTankBullet> TankbulletPool = new List<BossTankBullet>();
    [SerializeField] private int bulletMaxCount;
    private int curBulletIndex = 0;

    public GameObject parent;

    [Header("Hurt")]
    public GameObject hitBox;

    [Header("Health")]
    public int maxHp;
    public int hp;
    private bool isBerserk;
    public GameObject explosion;

    private int moveCount;
    private int curMoveCount = 0;

    private bool stop;


    void OnEnable()
    { 
        //오브젝트 풀링 셋팅
        for (int i = 0; i < bulletMaxCount; i++)
        {
            BossTankBullet b = Instantiate<BossTankBullet>(bullet);

            b.gameObject.SetActive(false);
            b.gameObject.transform.SetParent(parent.transform);
            TankbulletPool.Add(b);
        }

        //현재 상태 정의
        currentState = bossStates.shooting;
        hp = maxHp;
        
    }


    private void Update()
    {
        StartCoroutine(TankBossCo());
    }

    IEnumerator TankBossCo()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                if (!stop)
                {
                    shotCounter -= Time.deltaTime;

                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots;

                        TankbulletPool[curBulletIndex].transform.position = firePoint.position;
                        TankbulletPool[curBulletIndex].transform.localScale = theBoss.localScale;

                        TankbulletPool[curBulletIndex].gameObject.SetActive(true);

                        if (curBulletIndex >= bulletMaxCount - 1)
                        {
                            curBulletIndex = 0;
                            moveCount = Random.Range(1, 4);
                            anim.SetTrigger("Moving");

                            stop = true;
                            yield return new WaitForSeconds(1.25f);
                            hitBox.SetActive(false);
                            currentState = bossStates.moving;
                            stop = false;
                        }

                        else
                        {
                            curBulletIndex++;
                        }
                    }
                }
                break;
            case bossStates.moving:
                if (!stop)
                {
                    if (moveRight)
                    {
                        theBoss.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));

                        if (theBoss.position.x > rightPoint.position.x)
                        {
                            theBoss.localScale = new Vector3(1f, 1f, 1f);

                            moveRight = false;

                            curMoveCount++;
                        }
                    }
                    else
                    {
                        theBoss.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));

                        if (theBoss.position.x < leftPoint.position.x)
                        {
                            theBoss.localScale = new Vector3(-1f, 1f, 1f);

                            moveRight = true;
                            curMoveCount++;
                        }
                    }
                }
               
                if (curMoveCount == moveCount)
                {
                    curMoveCount = 0;
                    anim.SetTrigger("StopMoving");
                    stop = true;

                    yield return new WaitForSeconds(1.5f);

                    currentState = bossStates.shooting;
                    shotCounter = 0f;

                    hitBox.SetActive(true);
                    stop = false;
                }
                break;


            case bossStates.die:
                gameObject.SetActive(false);
                break;
        }
    }

    public void TakeHit()
    {
        tankSR.color = new Color(1f, 0.4f, 0.4f);
        Invoke("returnColor", 0.1f);

        hp--;
        anim.SetTrigger("Hit");

        if (hp <= 0)
        {
            currentState = bossStates.die;
        }
        else if (hp <= maxHp / 2 && !isBerserk)
        {
            timeBetweenShots /= shotSpeedUp;
            moveSpeed = moveSpeed * speedUp;
            isBerserk = true;
        }
    }

    private void Berserk()
    {
        timeBetweenShots /= shotSpeedUp;
        moveSpeed = moveSpeed * speedUp;
        isBerserk = true;
    }

    void returnColor()
    {
        tankSR.color = new Color(1, 1, 1);
    }

}
