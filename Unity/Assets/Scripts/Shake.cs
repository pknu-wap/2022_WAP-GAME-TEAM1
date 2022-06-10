using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float shakingTime;
    [SerializeField] AnimationCurve curve;
    private float curShakingTime;

    public void shaking()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        curShakingTime = 0f;

        while (curShakingTime < shakingTime)
        {
            curShakingTime += Time.deltaTime;
            float strength = curve.Evaluate(curShakingTime / shakingTime);
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }

        transform.position = startPosition;
    }
}
