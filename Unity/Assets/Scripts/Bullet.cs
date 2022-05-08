using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    void Start()
    {
        Invoke("BulletOff", 2f); //2ÃÊ µÚ¿¡ Bullet ¼Ò¸ê.
    }

    void Update() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
            BulletOff();

        if(transform.rotation.y==0)
            transform.Translate(transform.right * speed * Time.deltaTime);
        else
            transform.Translate(transform.right*-1 * speed * Time.deltaTime);
       
    }
    void BulletOff()
    {
        Destroy(gameObject);
    }

}
