using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public bool rotate;
    public float rotSpeed;
    
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
        }
    }

    public void startLotate()
    {
        rotate = true;
    }
}
