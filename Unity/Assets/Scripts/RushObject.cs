using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushObject : MonoBehaviour
{
    [SerializeField]
    private float RushSpeed;

    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform targetPos;

    [SerializeField]
    private bool isTriggered;
    [SerializeField]
    private bool movingTarget;

    private void Start()
    {
        startPos.parent = null;
        targetPos.parent = null;
    }

    void Update()
    {
        if (isTriggered)
        { 

            if (movingTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, RushSpeed * Time.deltaTime);
                if (transform.position == targetPos.position)
                { 
                    movingTarget = false;
                }
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos.position, (RushSpeed / 2f) * Time.deltaTime);
                if (transform.position == startPos.position)
                {
                    isTriggered = false;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTriggered = true;
            movingTarget = true;
        }
    }

    
}
