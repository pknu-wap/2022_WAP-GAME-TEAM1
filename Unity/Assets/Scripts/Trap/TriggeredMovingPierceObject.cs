using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredMovingPierceObject : Trap
{
    public float moveSpeed;
    public bool isTriggered;

    [SerializeField]
    private Transform targetPos;

    void Start()
    {
        isTriggered = false;
    }


    void Update()
    {
        if (isTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
        }
    }
}
