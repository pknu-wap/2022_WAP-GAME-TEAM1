using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3ToBossPattern2 : MonoBehaviour
{
    public float speed;

    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    void OnEnable()
    {
        StartCoroutine(startCo());
    }

    public void startEndCo()
    {
        StartCoroutine(endCo());
    }
    IEnumerator startCo()
    {
        while (true)
        {
            transform.Translate(new Vector2(0, -speed * Time.deltaTime));
            if (transform.position.y <= targetPos.y)
                break;
            yield return null;
        }
    }

    IEnumerator endCo()
    {
        while (true)
        {
            transform.Translate(new Vector2(0, speed * Time.deltaTime));
            if (transform.position.y >= startPos.y)
                break;
            yield return null;
        }
    }

}
