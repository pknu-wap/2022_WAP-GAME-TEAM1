using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 v2;
    private Rigidbody2D rb;
    public bool moving = false;

    void Start()
    {
        
    }

    void Awake()
    {
       rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
       rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    void Update()
    {
        if (moving) rb.velocity = v2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {        
            moving=true;
        }
    }
}
