using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    public Vector3 spawnPoint;
    public string lastSpawnMapName;

    public string nowMapName = "Map1";

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
        if (lastSpawnMapName != nowMapName)
        {
            lastSpawnMapName = nowMapName;
        }
    }
}
