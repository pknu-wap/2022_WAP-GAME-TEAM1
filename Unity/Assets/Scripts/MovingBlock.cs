using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 v2;
    private Rigidbody2D rb;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {        
            moving=true;
            //GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
