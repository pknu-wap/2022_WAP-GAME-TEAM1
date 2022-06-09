using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox3 : MonoBehaviour
{
    [SerializeField] Stage3Boss cont;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            cont.TakeHit();
        }
    }
}
