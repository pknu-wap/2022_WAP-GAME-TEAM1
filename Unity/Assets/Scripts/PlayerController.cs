using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    private bool canDoubleJump;
    private Rigidbody2D theRB;

    [SerializeField]
    private LayerMask groundLayer;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isGrounded;
    private Vector2 footPosition;

    private SpriteRenderer theSR;
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

        Bounds bounds = capsuleCollider2D.bounds;

        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
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
        else
        {
            theSR.flipX = false;
        }

        if (GameManager.instance.isDead)
        {
            gameObject.SetActive(false);
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("canDoubleJump", canDoubleJump);

      
    }
}
