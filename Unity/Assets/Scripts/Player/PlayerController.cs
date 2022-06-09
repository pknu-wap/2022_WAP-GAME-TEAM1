using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;
    public GameObject UIGameOver;
    //무빙블럭용
    public Vector2 blockSpeed;

    [SerializeField]
    // 이동 속도
    private float moveSpeed; 
    [SerializeField]
    // 점프력
    private float jumpForce;
    // 2단 점프 가능 여부
    private bool canDoubleJump;
    //rigidbody 컴포넌트
    public Rigidbody2D theRB;
    [SerializeField]
    // groundLayer 판단
    private LayerMask groundLayer;
    // 캡슐 콜라이더 
    private CapsuleCollider2D capsuleCollider2D;
    //발이 땅에 닿아있는지 여부 판단
    private bool isGrounded;
    //발의 포지션 
    [SerializeField] private GameObject footPosition;
    //애니메이터
    public Animator anim;
    //플레이어 죽음 이펙트
    public GameObject deadEffect;

    public bool CanMove;
    public bool isReverse;
    public bool isInvincible;



    void Awake()
    {
        if (instance == null)
        {
            theRB = GetComponent<Rigidbody2D>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            anim = GetComponent<Animator>();
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CanMove = true;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void Update()
    {

        Move();

        //플레이어의 좌우반전을 y축 회전을 이용하여 구현
        //총알 발사시에 rotation에 맞는 방향으로 발사하기 위해서
        Flip();

       
        // footPosition의 지름 0.1범위 가상의 원 범위를 설정해서 이 범위가 groundLayer에 닿아있으면 true를 반환
        isGrounded = Physics2D.OverlapCircle(footPosition.transform.position, 0.1f, groundLayer);
        
        //땅에 닿아있으면 2단 점프 가능 여부 초기화
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // 점프 관련 (Space로 점프)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅에 닿아있으면 점프 가능
            Jump();
        }

        //죽음관련 컨트롤 죽음 변수는 GameManager에서 컨트롤
        if (GameManager.instance.isDead && !isInvincible)
        {
            SoundManager.instance.PlaySFX(1);
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Instantiate(UIGameOver, new Vector3(CameraManager.instance.transform.position.x, CameraManager.instance.transform.position.y, CameraManager.instance.transform.position.z + 1), Quaternion.identity);
            SoundManager.instance.PlayGameOver();
            GameManager.instance.StartDeadCo();
        }

        //애니메이션 세팅
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("canDoubleJump", canDoubleJump);   
    }

    private void Move()
    {
        if (CanMove)
        {
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y)
                      + blockSpeed;
        }     
    }

    private void Jump()
    {
        if (CanMove)
        {
            if (isGrounded)
            {
                if (!isReverse)
                {
                    if (theRB.velocity.y < 0)
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    else
                        theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y + jumpForce);
                }
                else
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y - jumpForce);
                }
                SoundManager.instance.PlaySFX(0);

            }
            // canDoubleJump가 true면 공중에서 점프 한번 더 가능.
            else if (canDoubleJump)
            {
                if (!isReverse)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 0.8f);
                }
                else
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, -jumpForce * 0.8f);
                }
                canDoubleJump = false;
                SoundManager.instance.PlaySFX(0);
            }
        }
        
    }

    public void Bounce(float bounceForce)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }

    public void Reverse()
    {
        isReverse = true;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 180);
        theRB.gravityScale = -2f;
        theRB.velocity = new Vector2(theRB.velocity.x, 1f);
    }

    public void Forward()
    {
        isReverse = false;
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
        theRB.gravityScale = 2f;
        theRB.velocity = new Vector2(theRB.velocity.x, -5f);
    }

    public void Flip()
    {
        if (theRB.velocity.x > 0)
        {
            if (!isReverse)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            else
                transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
        }

        else if (theRB.velocity.x < 0)
        {
            if (!isReverse)
                transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
            else
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }
    }

}
