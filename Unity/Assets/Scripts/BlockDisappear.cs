using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDisappear : MonoBehaviour
{
    //플레이어와 충돌하면 삭제됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
	}
}
