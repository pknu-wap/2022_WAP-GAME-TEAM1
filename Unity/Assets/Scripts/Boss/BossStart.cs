using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {        
            CheckPointManager.instance.SetSpawnPoint(transform.position);
            SLManager.instance.Save();
            go.SetActive(true);
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
