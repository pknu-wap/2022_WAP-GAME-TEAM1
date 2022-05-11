using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void Update()
    {
          //충돌 판정
          
          //충돌하면 Kinematic 꺼지기
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name.Equals("Player")){
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
