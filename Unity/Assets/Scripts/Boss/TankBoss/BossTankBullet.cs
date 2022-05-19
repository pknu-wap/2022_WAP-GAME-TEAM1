using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankBullet : MonoBehaviour
{
    public float speed;

    void OnEnable()
    { 
        Invoke("BulletOff", 3f); //2ÃÊ µÚ¿¡ Bullet ¼Ò¸ê.
    }
    
    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    void BulletOff()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameManager.instance.isDead = true;
        BulletOff();
    }
}
