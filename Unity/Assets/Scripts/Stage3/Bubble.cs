using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed;
    private Rigidbody2D theRB;
    private Animator theAnim;
    private CircleCollider2D circleCollider;
    int randomNum;

    void OnEnable()
    {
        theRB = GetComponent<Rigidbody2D>();
        theAnim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;

        Shot();
        StartCoroutine(BubbleDestroyCo());
    }


    void Shot()
    {
        randomNum = Random.Range(0, 4);

        switch (randomNum)
        {
            case 0:
                theRB.velocity = new Vector2(speed, speed);
                break;
            case 1:
                theRB.velocity = new Vector2(speed, -speed);
                break;

            case 2:
                theRB.velocity = new Vector2(-speed, speed);
                break;

            case 3:
                theRB.velocity = new Vector2(-speed, -speed);
                break;
        }
    }
 

    IEnumerator BubbleDestroyCo()
    {
        yield return new WaitForSeconds(10f);
        circleCollider.enabled = false;
        theRB.velocity = Vector2.zero;
        theAnim.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.instance.isDead = true;
        }
    }

  
}
