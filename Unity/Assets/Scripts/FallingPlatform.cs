using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        }
    }
}
