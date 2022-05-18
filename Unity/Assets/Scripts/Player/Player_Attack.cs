using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Bullet bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;

    public GameObject parent;

    //ÃÑ¾Ë Object pool
    private List<Bullet> bulletPool = new List<Bullet>();

    //³»°¡ »ý¼ºÇÒ ÃÑ¾Ë °¹¼ö
    [SerializeField] private int bulletMaxCount;

    private int curBulletIndex = 0;

    void Start()
    {
        for (int i = 0; i < bulletMaxCount; i++)
        {
            Bullet b = Instantiate<Bullet>(bullet);

            b.gameObject.SetActive(false);
            b.gameObject.transform.SetParent(parent.transform);
            bulletPool.Add(b);
        }
    }

    void Update()
    {
        if(curtime<=0)
        {
            //Shift to Fire Bullet
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SoundManager.instance.PlaySFX(2);

                bulletPool[curBulletIndex].transform.position = pos.position;
                bulletPool[curBulletIndex].transform.rotation = transform.rotation;

                bulletPool[curBulletIndex].gameObject.SetActive(true);

                if (curBulletIndex >= bulletMaxCount - 1)
                {
                    curBulletIndex = 0;
                    curtime = cooltime;
                }
                else
                {
                    curBulletIndex++;
                }
            }
        }
        curtime -= Time.deltaTime;

    }
}
