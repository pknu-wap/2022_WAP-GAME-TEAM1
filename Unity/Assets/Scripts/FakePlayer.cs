using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    public GameObject go;
    public GameObject go2;
    private float Timer = 0f;
    private float Time1 = 2f;
    private float Time2 = 6f;
    private bool act1 = true;
    private bool act2 = true;

    void Update()
    {
        Timer += Time.deltaTime;
        if (act1 && Timer>Time1)
        {
            act1=false;
            Act1();
        }
        if (act2 && Timer>Time2)
        {
            act2=false;
            Act2();
        }
    }

    void Act1()
    {
        go.SetActive(true);
    }

    void Act2()
    {
        go.SetActive(false);
        go2.instance.startLotate();
    }
}
