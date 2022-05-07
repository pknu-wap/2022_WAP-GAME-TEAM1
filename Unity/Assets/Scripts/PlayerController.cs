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

        //�÷��̾��� �¿������ y�� ȸ���� �̿��Ͽ� ����
        //�Ѿ� �߻�ÿ� rotation�� �´� �������� �߻��ϱ� ���ؼ�
        var movement = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(0, movement))
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

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
            }
            // canDoubleJump�� true�� ���߿��� ���� �ѹ� �� ����.
            else if (canDoubleJump)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 0.8f);
                canDoubleJump = false;
            }
        }
        //�������� ��Ʈ�� ���� ������ GameManager���� ��Ʈ��
        if (GameManager.instance.isDead)
        {
            gameObject.SetActive(false);
            GameManager.instance.isDead = false;
        }

        //�ִϸ��̼� ����.
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("canDoubleJump", canDoubleJump);
    }
}
