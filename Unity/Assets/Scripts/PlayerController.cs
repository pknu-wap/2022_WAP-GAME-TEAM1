using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject UIGameOver;

    [SerializeField]
    // 이동 속도
    private float moveSpeed; 
    [SerializeField]
    // 점프력
    private float jumpForce;
    // 2단 점프 가능 여부
    private bool canDoubleJump;
    //rigidbody 컴포넌트
    private Rigidbody2D theRB;
    [SerializeField]
    // groundLayer 판단
    private LayerMask groundLayer;
    // MovingPlatformLayer 판단
    private LayerMask MovingPlatformLayer;
    // 캡슐 콜라이더 
    private CapsuleCollider2D capsuleCollider2D;
    //발이 땅에 닿아있는지 여부 판단
    private bool isGrounded;
    //발의 포지션 
    private Vector2 footPosition;
    //SpriteRenderer 컴포넌트
    private SpriteRenderer theSR;
    //애니메이터
    private Animator anim;

    //�÷��̾� ���� ����Ʈ
    public GameObject deadEffect;


    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theSR = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);

        //플레이어의 좌우반전을 y축 회전을 이용하여 구현
        //총알 발사시에 rotation에 맞는 방향으로 발사하기 위해서
        var movement = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(0, movement))
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        // capsuleCollider의 min, max, center등의 위치 정보를 나타냄
        Bounds bounds = capsuleCollider2D.bounds;
        
        //발의 포지션은 콜라이더 범위의 x축 중간, y축 최소 값.
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // footPosition의 지름 0.1범위 가상의 원 범위를 설정해서 이 범위가 groundLayer에 닿아있으면 true를 반환
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer) || Physics2D.OverlapCircle(footPosition, 0.1f, MovingPlatformLayer);

        //땅에 닿아있으면 2단 점프 가능 여부 초기화
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // 점프 관련 (Space로 점프)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅에 닿아있으면 점프 가능
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            // canDoubleJump가 true면 공중에서 점프 한번 더 가능.
            else if (canDoubleJump)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 0.8f);
                canDoubleJump = false;
            }
        }

        if (theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }


        //죽음관련 컨트롤 죽음 변수는 GameManager에서 컨트롤
        if (GameManager.instance.isDead)
        {
            SoundManager.instance.PlaySFX(1);
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            GameManager.instance.isDead = false;
            GameObject objUIGameOver = Instantiate(UIGameOver);
            objUIGameOver.transform.position = new Vector3(0,0,-1);
        }

        //애니메이션 세팅
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("canDoubleJump", canDoubleJump);
    }
}
