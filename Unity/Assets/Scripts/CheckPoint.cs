using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer theSR;

    public Sprite cpOn, cpOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            theSR.sprite = cpOn;
            CheckPointManager.instance.SetSpawnPoint(transform.position);


        }
    }

    public void ResetCheckPoint()
    {
        theSR.sprite = cpOff;
    }

}
