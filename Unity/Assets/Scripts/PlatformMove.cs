using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public Transform startPos; 
    public Transform endPos;
    public Transform platformPos;
    public float MoveSpeed;

    void Start()
    {
        transform.position = startPos.position;
        platformPos = endPos;
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, platformPos.position, Time.deltaTime * MoveSpeed);

        if(Vector2.Distance(transform.position,platformPos.position)<=0.05f)
        {
            if (platformPos == endPos) platformPos = startPos;
            else platformPos = endPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
