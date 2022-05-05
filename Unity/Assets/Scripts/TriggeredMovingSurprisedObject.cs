using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredMovingSurprisedObject : Trap
{

    public Sprite objectSprite;
    private SpriteRenderer theSR;
    [SerializeField]
    private Transform targetPos;

    public float moveSpeed;
    public bool isTriggered;

    

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isTriggered)
        {   
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            if (transform.position.y > 20.0f || transform.position.y < 20.0f || transform.position.x > 20.0f || transform.position.x < -20.0f)
            {
                Destroy(gameObject);
            }      
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            theSR.sprite = objectSprite;
            isTriggered = true;
        }
    }
}
