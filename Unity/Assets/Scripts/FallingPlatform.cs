using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            rb.gravityScale = 1;
        }
    }
}
