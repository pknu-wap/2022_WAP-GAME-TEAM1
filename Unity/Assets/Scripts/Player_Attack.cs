using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;

    void Start()
    {
        
    }

    void Update()
    {
        if(curtime<=0)
        {
            //Shift to Fire Bullet
            if (Input.GetKey(KeyCode.LeftShift)) 
                Instantiate(bullet, pos.position, transform.rotation);

            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

    }
}
