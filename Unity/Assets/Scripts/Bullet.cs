using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D theRB;
    void Start()
    {
        Invoke("BulletOff", 2); //2ÃÊ µÚ¿¡ Bullet ¼Ò¸ê.
    }

    void Update() {
        if(transform.rotation.y==0)
            transform.Translate(transform.right * speed * Time.deltaTime);
        else
            transform.Translate(transform.right*-1 * speed * Time.deltaTime);

    }
    void BulletOff()
    {
        Destroy(gameObject);
    }
    /* private void BulletCollision(Collision2D collision)
     * {
     * Destroy(gameObject);*/
}
