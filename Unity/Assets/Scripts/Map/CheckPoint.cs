using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer theSR;

    public Sprite cpOn, cpOff;

    public int nowMapIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(CheckPointCo());
        }
    }

    public void ResetCheckPoint()
    {
        theSR.sprite = cpOff;
    }
    
    private IEnumerator CheckPointCo()
    {
        theSR.sprite = cpOn;
        CheckPointManager.instance.SetSpawnPoint(transform.position, CameraManager.instance.transform.position, nowMapIndex, SoundManager.instance.lastPlayingBGMIndex);
        SLManager.instance.Save();

        yield return new WaitForSeconds(1.5f);

        theSR.sprite = cpOff;
    }

}
