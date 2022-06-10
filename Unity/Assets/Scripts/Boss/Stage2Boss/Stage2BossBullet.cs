using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossBullet : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        Invoke("BulletOff", 3f);
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    void BulletOff()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletOff();
    }
}
