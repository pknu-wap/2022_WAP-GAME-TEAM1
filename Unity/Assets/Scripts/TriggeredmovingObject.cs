using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredmovingObject : Spike
{
    public static TriggeredmovingObject instance;
    public float moveSpeed;
    public bool isTriggered;
    public float rayDistance;

    [SerializeField]
    private Transform targetPos;
  


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isTriggered = false;
        targetPos.parent = null;
    }

    
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!isTriggered)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos.position - transform.position, rayDistance, 1 << LayerMask.NameToLayer("Player"));

            Debug.DrawRay(transform.position, Vector2.up * rayDistance, Color.red);

            if (hit.transform != null)
            {
                isTriggered = true;
            }
        }

        if (isTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        }
    }
}
