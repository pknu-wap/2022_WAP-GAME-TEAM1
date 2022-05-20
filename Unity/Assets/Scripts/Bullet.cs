using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    void OnEnable()
    {
        Invoke("BulletOff", 2f); //2�� �ڿ� Bullet �Ҹ�.
    }

    void Update() {
        if (transform.rotation.y == 0)
            transform.Translate(transform.right * speed * Time.deltaTime);
        else
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
       
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
