using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    // �̵� �ӵ�
    private float moveSpeed; 
    [SerializeField]
    // ������
    private float jumpForce;
    // 2�� ���� ���� ����
    private bool canDoubleJump;
    //rigidbody ������Ʈ 
    private Rigidbody2D theRB;

    [SerializeField]
    // groundLayer �Ǵ�
    private LayerMask groundLayer;
    // ĸ�� �ݶ��̴� 
    private CapsuleCollider2D capsuleCollider2D;
    //���� ���� ����ִ��� ���� �Ǵ�
    private bool isGrounded;
    //���� ������ 
    private Vector2 footPosition;

    //SpriteRenderer ������Ʈ
    private SpriteRenderer theSR;
    //�ִϸ�����
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
        // �¿� �̵����� �ڵ�
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);

        // capsuleCollider�� min, max, center���� ��ġ ������ ��Ÿ��
        Bounds bounds = capsuleCollider2D.bounds;

        //���� �������� �ݶ��̴� ������ x�� �߰�, y�� �ּ� ��.
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // footPosition�� ���� 0.1���� ������ �� ������ �����ؼ� �� ������ groundLayer�� ��������� true�� ��ȯ
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        //���� ��������� 2�� ���� ���� ���� �ʱ�ȭ
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        
        // ���� ���� (Space�� ����)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���� ��������� ���� ����
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                SoundManager.instance.PlaySFX(0);
            }
            // canDoubleJump�� true�� ���߿��� ���� �ѹ� �� ����.
            else if (canDoubleJump)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 0.8f);
                canDoubleJump = false;
                SoundManager.instance.PlaySFX(0);
            }
        }

        // �÷��̾� ��������Ʈ ���� �ڵ� x���� �ӵ��� 0�̸��̸� ��������Ʈ�� �����´�.
        if (theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }

        //�������� ��Ʈ�� ���� ������ GameManager���� ��Ʈ��
        if (GameManager.instance.isDead)
        {
            SoundManager.instance.PlaySFX(1);
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            GameManager.instance.isDead = false;
        }

        //�ִϸ��̼� ����.
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("canDoubleJump", canDoubleJump);

      
    }
}
