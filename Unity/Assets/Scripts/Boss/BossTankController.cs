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
    

    void Start()
    { 
        anim = GetComponent<Animator>();

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
    }
    
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
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
                        currentState = bossStates.moving;
                    }
                    else
                    {
                        curBulletIndex++;
                    }
                }
                break;
            case bossStates.moving:
                int moveCount = Random.Range(1, 4);
                for (int i = 0; i < moveCount; i++)
                {
                    if (moveRight)
                    {
                        theBoss.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
                        //theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                        if (theBoss.position.x > rightPoint.position.x)
                        {
                            theBoss.localScale = new Vector3(1f, 1f, 1f);

                            moveRight = false;
                        }
                    }
                    else
                    {
                        theBoss.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
                        //theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                        if (theBoss.position.x < leftPoint.position.x)
                        {
                            theBoss.localScale = new Vector3(-1f, 1f, 1f);

                            moveRight = true;
                        }
                    }
                }
                EndMoveMent();
                break;


            case bossStates.die:
                gameObject.SetActive(false);
                break;
        }
       
    }

    public void TakeHit()
    {
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

    private void EndMoveMent()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }

    private void Berserk()
    {
        timeBetweenShots /= shotSpeedUp;
        moveSpeed = moveSpeed * speedUp;
        isBerserk = true;
    }
}
