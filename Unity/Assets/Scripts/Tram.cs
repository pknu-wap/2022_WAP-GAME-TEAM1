using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tram : MonoBehaviour
{
    public Vector2 v2;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    //무빙블럭 위에 플레이어가 있으면 플레이어도 같이 이동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.blockSpeed = new Vector2(v2.x, 0);
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.blockSpeed = new Vector2(0,0);
        }
        if (collision.CompareTag("DeleteBlock"))
        {
            Destroy(gameObject);
        }
    }
}
