using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBubble : MonoBehaviour
{
    [SerializeField] GameObject Bubble;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform bubblePool;

    [SerializeField] float shotCounter;
    private float curShotCounter;

    private int curShotIndex;

    private Animator _anim;

    GameObject[] Bubbles = new GameObject[30];

    void OnEnable()
    {
        curShotCounter = 0;
        curShotIndex = 0;
        _anim = GetComponent<Animator>();
        for (int i = 0; i < Bubbles.Length; i++)
        {
            Bubbles[i] = Instantiate(Bubble);
            Bubbles[i].transform.SetParent(bubblePool);
            Bubbles[i].SetActive(false);
        }

    }

    void Update()
    {
        if (curShotCounter >= shotCounter)
        {
            _anim.SetTrigger("Shot");
            SoundManager.instance.PlayBossSFX(13);

            Bubbles[curShotIndex].SetActive(true);
            Bubbles[curShotIndex++].transform.position = firePoint.position;
            if (curShotIndex >= 20) curShotIndex = 0;

            curShotCounter = 0;
        }
        curShotCounter += Time.deltaTime;
    }
}
