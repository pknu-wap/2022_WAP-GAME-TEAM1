using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
