using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossPattern2 : MonoBehaviour
{
    [SerializeField] float Xspeed;
    [SerializeField] float Yspeed;
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    void Update()
    {
        transform.Translate(new Vector2(-Xspeed * Time.deltaTime, -Yspeed * Time.deltaTime));

        if (transform.position.x <= targetPos.x)
        {
            transform.position = startPos;
            gameObject.SetActive(false);
        }
    }
}
