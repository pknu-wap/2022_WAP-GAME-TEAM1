using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredmovingObject : Trap
{
    public float moveSpeed;
    public bool isTriggered;

    [SerializeField]
    private Transform targetPos;

    void Start()
    {
        isTriggered = false;
        targetPos.parent = null;
    }

    
    void Update()
    {
        //Physics2D.queriesStartInColliders = false;
        //if (!isTriggered)
        //{

        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPos.position - transform.position, rayDistance, 1 << LayerMask.NameToLayer("Player"));

        //    Debug.DrawRay(transform.position, Vector2.up * rayDistance, Color.red);

        //    if (hit.transform != null)
        //    {
        //        isTriggered = true;
        //    }
        //}

        // 트리거되면 targetPos쪽으로 moveSpeed속도로 움직임.
        if (isTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            
        }
    }

    // 설정한 영역에 닿으면 Trigger변수를 true해서 트랩 동작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
        }    
    }
}
