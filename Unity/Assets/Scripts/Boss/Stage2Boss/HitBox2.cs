using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox2 : MonoBehaviour
{
    [SerializeField] Stage2BossController _stage2BossController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            _stage2BossController.TakeHit();
        }
    }
 
}
