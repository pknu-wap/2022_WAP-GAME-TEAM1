using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossController : MonoBehaviour
{
    [SerializeField] Transform BossTransform;
    [SerializeField] Transform GroundCheck;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    [SerializeField] Rigidbody2D BossRB;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] SpriteRenderer BossSR;
    [SerializeField] GameObject rightWall;
    [SerializeField] SpriteRenderer blueBackGround;
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject transferMap;
    
    [Header("Attack")]
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] short rushCount;
    [SerializeField] float rotSpeed;
    [SerializeField] Stage2BossBullet bullet;
    [SerializeField] short MaxshotCount;
    [SerializeField] float maxMultiShotCount;
    [SerializeField] float multiShotDelay;
    private float curMultiShotCount;
    private short curShotCount;
    private Stage2BossBullet[] bulletPool;
    private int curBulletIndex;
    private float _delay;

    [Header("Health")]
    [SerializeField] short maxHp;
    [SerializeField] short currentHp;
    [SerializeField] bool isBerserk;
    [SerializeField] bool isInvincible;
    [SerializeField] float speedUp;

    private bool isGround;
    private short jumpCount;
    private bool moveRight;
    private short curRushCount;
    private bool isRush;
    private short multiShotType;
    private bool Phase2;
    private RandomAttack randomAttack;
    

    enum RandomAttack
    {
       Select,
       Berserk,
       Move,
       Rush,
       Shot,
       MultiShot,
       Die
    }


    void OnEnable()
    {
        SoundManager.instance.StopBGM(2);
        jumpCount = 0;
        randomAttack = RandomAttack.Move;
        curShotCount = 0;
        currentHp = maxHp;
        StartCoroutine(BossEventCo1());
    }

    IEnumerator BossEventCo1()
    {
        PlayerController.instance.CanMove = false;
        PlayerController.instance.theRB.velocity = Vector2.zero;
        while (true)
        {
            isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);
            if (isGround && jumpCount < 5)
            {
                BossRB.velocity = new Vector2(-1f, 5f);
                jumpCount += 1;
            }
            else if (isGround && jumpCount >= 5)
            {
                BossRB.velocity = new Vector2(0f, 5f);
                break;
            }
            yield return null;
        }

        StartCoroutine(BossEventCo2());
    }

    IEnumerator BossEventCo2()
    {
        yield return new WaitForSeconds(2f);

        SoundManager.instance.PlayBossSFX(4);
        while (true)
        {
            if (BossTransform.localScale.x < 3f || BossTransform.localScale.y < 3f)
            {
                BossTransform.localScale = new Vector2(BossTransform.localScale.x + 0.6f * Time.deltaTime, BossTransform.localScale.y + 0.6f * Time.deltaTime);
            }
            else
            {
                SoundManager.instance.StopBossSFX(4);
                break;
            }
            yield return null;
        }

        SoundManager.instance.PlayBGM(3);
        PlayerController.instance.CanMove = true;
        rightWall.gameObject.SetActive(true);
        BossRB.gravityScale = 5f;
        StartCoroutine(BossBattleCo());
    }

    IEnumerator BossBattleCo()
    {
        while (true)
        {
            switch (randomAttack)
            {
                case RandomAttack.Move:
                    isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);

                    if (moveRight)
                    {
                        if (isGround)
                        {
                            BossRB.velocity = new Vector2(moveSpeed.x, Random.Range(moveSpeed.y, moveSpeed.y + 5));
                        }

                        if (BossTransform.position.x > rightPoint.position.x)
                        {
                            moveRight = false;
                            randomAttack = RandomAttack.Select;
                        }
                    }
                    else
                    {
                        if (isGround)
                        {
                            BossRB.velocity = new Vector2(-moveSpeed.x, Random.Range(moveSpeed.y, moveSpeed.y + 5));
                        }

                        if (BossTransform.position.x < leftPoint.position.x)
                        {
                            moveRight = true;
                            randomAttack = RandomAttack.Select;
                        }
                    }
                    break;

                case RandomAttack.Select:
                    BossRB.gravityScale = 0f;
                    BossRB.velocity = new Vector2(0f, 5f);

                    if (BossTransform.position.y >= -35f)
                    {
                        randomAttack = (RandomAttack)Random.Range(3, 5);
                        
                        // 돌진
                        if (randomAttack == RandomAttack.Rush)
                        {
                            BossRB.velocity = (PlayerController.instance.transform.position - BossTransform.position).normalized * rushSpeed;
                        }
                        else if (randomAttack == RandomAttack.Shot)
                        {
                            BossRB.velocity = new Vector2(0f, 0f);
                        }
                    }
                    break;

                case RandomAttack.Rush:
                    isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);

                    if (isGround)
                    {
                        BossRB.gravityScale = 5f;
                        randomAttack = RandomAttack.Move;
                    }
                    break;

                case RandomAttack.Shot:
                    if (curShotCount < MaxshotCount)
                    {
                        Vector2 dir = PlayerController.instance.transform.position - BossTransform.position;

                        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        SoundManager.instance.PlayBossSFX(5);
                        GroundCheck.eulerAngles = new Vector3(0, 0, angle);
                        var _bullet = Instantiate(bullet, GroundCheck.position, GroundCheck.rotation);

                        curShotCount += 1;
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        curShotCount = 0;
                        BossRB.gravityScale = 5f;
                        yield return new WaitForSeconds(1f);
                        randomAttack = RandomAttack.Move;
                    }
                    break;

                case RandomAttack.Berserk:
                    BossRB.gravityScale = 0f;
                    if (BossTransform.position.y >= -35f)
                    {
                        BossRB.velocity = new Vector2(0f, 0f);
                        if (blueBackGround.color.a <= 0f)
                        {
                            yield return new WaitForSeconds(1f);

                            Battle2Init();
                        }
                        else
                        {
                            blueBackGround.color = new Color(1f, 1f, 1f, blueBackGround.color.a - 0.2f * Time.deltaTime);
                        }
                    }
                    else
                    {
                        BossTransform.eulerAngles = new Vector3(0, 0, BossTransform.eulerAngles.z + 150 * Time.deltaTime);
                    }
                 break;
            }
            if (Phase2)
                break;
            yield return null;
        }   
    }

    IEnumerator BossBattleCo2()
    {
        while (true)
        {
            switch (randomAttack)
            {
                case RandomAttack.Select:
                    BossRB.gravityScale = 0f;
                    BossRB.velocity = new Vector2(0f, 5f * speedUp);

                    if (BossTransform.position.y >= -35f)
                    {
                        if (currentHp <= 0)
                        {
                            SoundManager.instance.StopBGM(4);
                            BossRB.velocity = new Vector2(0f, 0f);
                            randomAttack = RandomAttack.Die;
                            break;
                        }

                        if (!isRush)
                        {
                            randomAttack = (RandomAttack)Random.Range(3, 6);
                        }
                        else
                        {
                            randomAttack = RandomAttack.Rush;
                        }

                        // 돌진
                        if (randomAttack == RandomAttack.Rush)
                        {
                            isRush = true;
                            BossRB.velocity = (PlayerController.instance.transform.position - BossTransform.position).normalized * rushSpeed;
                        }
                        else
                        {
                            BossRB.velocity = new Vector2(0f, 0f);
                            if (randomAttack == RandomAttack.MultiShot)
                            {
                                multiShotType = (short)Random.Range(0, 2);
                            }
                        }
                    }
                    break;

                case RandomAttack.Move:
                    isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);

                    if (moveRight)
                    {
                        if (isGround)
                        {
                            BossRB.velocity = new Vector2(Random.Range(moveSpeed.x, moveSpeed.x + 2), Random.Range(moveSpeed.y, moveSpeed.y + 5));
                        }

                        if (BossTransform.position.x > rightPoint.position.x)
                        {
                            moveRight = false;
                            randomAttack = RandomAttack.Select;
                        }
                    }
                    else
                    {
                        if (isGround)
                        {
                            BossRB.velocity = new Vector2(Random.Range(-moveSpeed.x, -moveSpeed.x - 2), Random.Range(moveSpeed.y, moveSpeed.y + 5));
                        }

                        if (BossTransform.position.x < leftPoint.position.x)
                        {
                            moveRight = true;
                            randomAttack = RandomAttack.Select;
                        }
                    }
                    break;

                case RandomAttack.Rush:
                    isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);

                    if (isGround){
                        if (curRushCount < rushCount - 1)
                        {
                            curRushCount += 1;
                            randomAttack = RandomAttack.Select;
                        }
                        else
                        {
                            isRush = false;
                            curRushCount = 0;
                            BossRB.gravityScale = 5f;
                            randomAttack = RandomAttack.Move;
                        }
                    }
                    break;

                case RandomAttack.Shot:
                    if (curShotCount < MaxshotCount)
                    {
                        Vector2 dir = PlayerController.instance.transform.position - BossTransform.position;

                        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        SoundManager.instance.PlayBossSFX(5);
                        GroundCheck.eulerAngles = new Vector3(0, 0, angle);
                        Stage2BossBullet _bullet = Instantiate(bullet, GroundCheck.position, GroundCheck.rotation);
                        _bullet.speed = 40;

                        curShotCount += 1;
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        curShotCount = 0;
                        BossRB.gravityScale = 5f;
                        yield return new WaitForSeconds(0.5f);
                        randomAttack = RandomAttack.Move;
                    }
                    break;

                case RandomAttack.MultiShot:
                    if (multiShotType == 0)
                    {
                        if (curMultiShotCount < maxMultiShotCount)
                        {
                            if (_delay >= 0.5f)
                            {
                                SoundManager.instance.PlayBossSFX(5);
                                for (int i = 0; i < 360; i += 13)
                                {
                                    //총알 생성
                                    bulletPool[curBulletIndex].gameObject.SetActive(true);

                                    bulletPool[curBulletIndex].speed = 15f;

                                    //총알 생성 위치를 (0,0) 좌표로 한다.
                                    bulletPool[curBulletIndex].transform.position = BossTransform.position;

                                    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                                    bulletPool[curBulletIndex++].transform.rotation = Quaternion.Euler(0, 0, i);

                                    if (curBulletIndex == 300)
                                        curBulletIndex = 0;
                                }
                                _delay = 0;
                            }
                            curMultiShotCount += Time.deltaTime;
                            _delay += Time.deltaTime;
                        }
                        else
                        {
                            curMultiShotCount = 0;
                            BossRB.gravityScale = 5f;
                            yield return new WaitForSeconds(0.5f);
                            randomAttack = RandomAttack.Move;
                        }
                    }
                    else
                    {
                        if (curMultiShotCount < maxMultiShotCount)
                        {
                            BossTransform.Rotate(Vector3.forward * rotSpeed * 100 * Time.deltaTime);

                            if (_delay >= multiShotDelay)
                            {
                                SoundManager.instance.bossSoundEffects[5].Play();
                                //총알 생성
                                bulletPool[curBulletIndex].gameObject.SetActive(true);

                                bulletPool[curBulletIndex].speed = 30f;

                                bulletPool[curBulletIndex].transform.position = BossTransform.position;

                                //총알의 방향을 오브젝트의 방향으로 한다.
                                //->해당 오브젝트가 오브젝트가 360도 회전하고 있으므로, Rotation이 방향이 됨.
                                bulletPool[curBulletIndex++].transform.rotation = BossTransform.rotation;

                                if (curBulletIndex == 300)
                                    curBulletIndex = 0;
                                _delay = 0;
                            }
       
                            curMultiShotCount += Time.deltaTime;
                            _delay += Time.deltaTime;
                        }

                        else
                        {
                            BossTransform.eulerAngles = new Vector3(0, 0, 0);
                            curMultiShotCount = 0;
                            BossRB.gravityScale = 5f;
                            yield return new WaitForSeconds(0.5f);
                            randomAttack = RandomAttack.Move;
                        }
                    }
                    break;

                case RandomAttack.Die:
                    yield return new WaitForSeconds(2f);
                    SoundManager.instance.PlayBossSFX(6);
                    gameObject.SetActive(false);
                    Instantiate(deathEffect, BossTransform.position, Quaternion.identity);
                    transferMap.SetActive(true);
                    break;
            }

            yield return null;
        }
    }

    public void TakeHit()
    {
        if (!isInvincible)
        {
            currentHp--;

            BossSR.color = new Color(1f, 1f, 1f, 0.5f);
            Invoke("returnColor", 0.1f);

            if (!isBerserk && currentHp <= maxHp / 2)
            {
                isBerserk = true;
                isInvincible = true;
                SoundManager.instance.StopBGM(3);
                BossRB.velocity = new Vector2(0f, 1.5f);
                randomAttack = RandomAttack.Berserk;
            }
        }
    }

    void returnColor()
    {
        if (!isBerserk)
            BossSR.color = new Color(1f, 1f, 1f, 1f);
        else
            BossSR.color = new Color(1f, 0f, 0f, 1f);
    }

    void Battle2Init()
    {
        bulletPool = new Stage2BossBullet[300];
        for (int i = 0; i < 300; i++)
        {
            bulletPool[i] = Instantiate(bullet);
            bulletPool[i].speed = 10f;
            bulletPool[i].gameObject.SetActive(false);
        }
        rushSpeed *= speedUp;
        moveSpeed *= speedUp;
        MaxshotCount = 5;
        curRushCount = 0;
        curBulletIndex = 0;
        _delay = 0;

        isInvincible = false;

        BossTransform.eulerAngles = new Vector3(0, 0, 0);
        SoundManager.instance.PlayBGM(4);
        multiShotType = (short)Random.Range(0, 2);
        randomAttack = RandomAttack.MultiShot;
        Phase2 = true;
        StartCoroutine(BossBattleCo2());
    }
}
