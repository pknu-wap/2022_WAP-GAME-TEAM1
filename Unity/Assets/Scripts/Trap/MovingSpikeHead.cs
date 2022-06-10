using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikeHead : MonoBehaviour
{
    float speed = 0.08f;
    float moveSpan = 0.01f;
    float moveDelta = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //특정 시간동안 speed만큼 좌우로 좌표 이동
        this.moveDelta += Time.deltaTime;
        if(this.moveDelta > this.moveSpan){
            gameObject.transform.Translate(0, speed, 0);
            this.moveDelta = 0;
        }

        //위 아래로 움직이기
        if(gameObject.transform.position.y >= 40f)
            speed = -0.08f;
        else if(gameObject.transform.position.y <= 35.5f)
            speed = 0.08f;
    }
}
