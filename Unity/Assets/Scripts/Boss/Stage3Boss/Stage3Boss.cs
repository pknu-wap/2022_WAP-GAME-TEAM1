using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Boss : Enemy
{
    enum BossState
    {
        Move,
        PatternSelect,
        Pattern1,
        Pattern2,
        Die
    }

    [SerializeField] GameObject nextMapPortal;
    [SerializeField] GameObject HurryUp;
    [SerializeField] GameObject Warning;

    [SerializeField] GameObject bulletParent;
    [SerializeField] GameObject bullet;

    [SerializeField] int bulletCount;
    private int curBulletIndex;

    private List<GameObject> bulletPool = new List<GameObject>();

    [SerializeField] private float shotCounter;
    private float curShotCounter;

    [Header("상태")]
    [SerializeField] private float currentHp;
    [SerializeField] float speedUp;
    [SerializeField] BossState bossState;

    [Header("패턴 딜레이")]
    [SerializeField] float patternDelay;
    private float curPatternDelay;

    [Header("패턴 1")]
    [SerializeField] RotationObject[] bossPattern1;
    [SerializeField] float[] rotationSpeed;

    [Header("패턴 2")]
    [SerializeField] Transform rightPos;
    [SerializeField] GameObject _collider;
    [SerializeField] GameObject[] bossPattern2;
    [SerializeField] Stage3ToBossPattern2 toBossPattern2;


    [SerializeField] GameObject _instance;
    [SerializeField] Trap trap;
    [SerializeField] Shake shake;
 
    private bool isInvincible;
    private int randomNum;


    void Start()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            bulletPool.Add(Instantiate(bullet));
            bulletPool[i].gameObject.transform.SetParent(bulletParent.transform);
            bulletPool[i].gameObject.SetActive(false);
            
        }
        curShotCounter = 0;
        enemyRB.velocity = new Vector2(-moveSpeed, moveSpeed);
        currentHp = hp;

        SoundManager.instance.PlayBGM(7);

        StartCoroutine(BossBattleCo());
    }


    IEnumerator BossBattleCo()
    {
        while (true)
        {
            if (enemyRB.velocity.x > 0) enemyTransform.localScale = new Vector2(-4f, 4f);
            else enemyTransform.localScale = new Vector2(4f, 4f);

            commonAttack();
            curShotCounter += Time.deltaTime;

            if (currentHp <= hp / 2)
            {
                StartCoroutine(Phase2());
                break;
            }
            yield return null;
        }
    }

    IEnumerator Phase2()
    {
        Phase2Init();

        while (true)
        {
            if (enemyRB.velocity.x > 0) enemyTransform.localScale = new Vector2(-4f, 4f);
            else enemyTransform.localScale = new Vector2(4f, 4f);

            switch (bossState)
            {
                case BossState.Move:
                    commonAttack();
                    if (curPatternDelay >= patternDelay)
                    {
                        enemyRB.velocity = Vector2.zero;
                        randomNum = Random.Range(2, 4);
                        bossState = BossState.PatternSelect;
                        curPatternDelay = 0;
                        if (randomNum == 3)
                        {
                            _collider.SetActive(false);
                            Warning.SetActive(true);
                            yield return new WaitForSeconds(2f);
                            toBossPattern2.gameObject.SetActive(true);
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    curShotCounter += Time.deltaTime;
                    curPatternDelay += Time.deltaTime;
                    break;

                case BossState.PatternSelect:
                    if (randomNum == 2)
                    {
                        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, centerPos.position, moveSpeed * Time.deltaTime);
                        if (enemyTransform.position == centerPos.position)
                        {
                            bossState = BossState.Pattern1;
                        }
                            
                    }
                    else
                    {
                        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, rightPos.position, moveSpeed * Time.deltaTime);
                        if (enemyTransform.position == rightPos.position)
                        {
                            isInvincible = true;
                            enemyTransform.localScale = new Vector2(4f, 4f);
                            bossState = BossState.Pattern2;
                        }
                    }
                    break;

                case BossState.Pattern1:
                    yield return new WaitForSeconds(0.5f);

                    SoundManager.instance.PlayBossSFX(10);

                    int randPattern1 = Random.Range(0, 2);
                    bossPattern1[randPattern1].gameObject.SetActive(true);
                    bossPattern1[randPattern1].rotationSpeed = 0f;

                    yield return new WaitForSeconds(1.5f);

                    bossPattern1[randPattern1].rotationSpeed = rotationSpeed[Random.Range(0, 2)];

                    yield return new WaitForSeconds(5f);

                    bossPattern1[randPattern1].rotationSpeed = -bossPattern1[randPattern1].rotationSpeed;

                    yield return new WaitForSeconds(5f);

                    bossPattern1[randPattern1].gameObject.SetActive(false);
                    enemyRB.velocity = new Vector2(-moveSpeed, moveSpeed);

                    curShotCounter = 0;
                    bossState = BossState.Move;
                    break;

                case BossState.Pattern2:
                    yield return new WaitForSeconds(1.5f);

                    int randPattern2 = Random.Range(0, 2);
                    bossPattern2[randPattern2].SetActive(true);

                    yield return new WaitForSeconds(5f);

                    toBossPattern2.startEndCo();
                    isInvincible = false;

                    enemyRB.velocity = new Vector2(-moveSpeed, moveSpeed);
                    yield return new WaitForSeconds(0.5f);

                    _collider.SetActive(true);
                    curShotCounter = 0;
                    bossState = BossState.Move;
                    break;

                case BossState.Die:
                    enemySR.color = new Color(1f, 1f, 1f, 1f);
                    break;
            }
         
            yield return null;
        }
    }

    IEnumerator DeadCo()
    {
        SoundManager.instance.StopBGM(8);
        shake.shaking();
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlayBossSFX(12);
        PlayerController.instance.CanMove = true;
        nextMapPortal.SetActive(true);
        gameObject.SetActive(false);
    }

    void commonAttack()
    {
        if (curShotCounter >= shotCounter)
        {
            SoundManager.instance.PlayBossSFX(9);
            if (enemyTransform.localScale.x > 0)
            {
                for (int i = 120; i < 240; i += 20)
                {
                    //총알 생성
                    bulletPool[curBulletIndex].gameObject.SetActive(true);

                    //총알 생성 위치를 (0,0) 좌표로 한다.
                    bulletPool[curBulletIndex].transform.position = enemyTransform.position;

                    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                    bulletPool[curBulletIndex++].transform.rotation = Quaternion.Euler(0, 0, i);

                    if (curBulletIndex == 30)
                        curBulletIndex = 0;
                }
            }
            else
            {
                for (int i = 60; i > -60; i -= 20)
                {
                    //총알 생성
                    bulletPool[curBulletIndex].gameObject.SetActive(true);

                    //총알 생성 위치를 (0,0) 좌표로 한다.
                    bulletPool[curBulletIndex].transform.position = enemyTransform.position;

                    //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                    bulletPool[curBulletIndex++].transform.rotation = Quaternion.Euler(0, 0, i);

                    if (curBulletIndex == 30)
                        curBulletIndex = 0;
                }
            }
            
            curShotCounter = 0;
        }
    }

    public void TakeHit()
    {
        if (!isInvincible)
        {
            currentHp--;

            if (currentHp > hp / 2)
                enemySR.color = new Color(1f, 1f, 1f, 0.5f);
            else
                enemySR.color = new Color(1f, 0.2f, 0.2f, 0.5f);

            Invoke("returnColor", 0.1f);
        }

        if (currentHp <= 0)
        {
            enemyRB.velocity = Vector2.zero;
            currentHp = 0;
            Destroy(trap);
            isInvincible = true;
            _instance.AddComponent<Stage3BossDie>();
            _anim.SetTrigger("Die");
            bossState = BossState.Die;
        }
    }

    void returnColor()
    {
        if (currentHp > hp / 2)
            enemySR.color = new Color(1f, 1f, 1f, 1f);
        else 
            enemySR.color = new Color(1f, 0.2f, 0.2f, 1f);
    }

    void Phase2Init()
    {
        SoundManager.instance.StopBGM(7);
        HurryUp.gameObject.SetActive(true);
        shotCounter /= speedUp;
        enemyRB.velocity = enemyRB.velocity * new Vector2(speedUp, speedUp);
        moveSpeed *= speedUp;
        curPatternDelay = 0;
    }

    public void StartDeadCo()
    {
        StartCoroutine(DeadCo());
    }
}
