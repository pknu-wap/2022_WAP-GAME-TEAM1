using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 v2;
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    public bool moving;

    void Start()
    {
        
    }

    void Awake()
    {
       rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (moving) rb.velocity = v2;
    }

    //무빙블럭 위에 플레이어가 있으면 플레이어도 같이 이동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {        
            moving=true;
            PlayerController.instance.blockSpeed = v2;
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {        
            PlayerController.instance.blockSpeed = new Vector2(0,0);
        }
    }
}
