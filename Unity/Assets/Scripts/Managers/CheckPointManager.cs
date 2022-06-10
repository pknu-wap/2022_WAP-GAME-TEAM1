using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    public Vector3 spawnPoint;
    public Vector3 lastCameraPosition;
    public string lastSpawnMapName;
    public int nowMapIndex;

    public string nowMapName;

    public GameObject Map;


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

    public void SetSpawnPoint(Vector3 newSpawnPoint, Vector3 newCameraPosition, int newMapIndex, int nowPlayingBGMIndex)
    {
        spawnPoint = newSpawnPoint;
        lastCameraPosition = newCameraPosition;
        lastSpawnMapName = nowMapName;
        nowMapIndex = newMapIndex;
        SoundManager.instance.lastPlayingBGMIndex = nowPlayingBGMIndex;
    }

    public void checkPointSpawn()
    {
        Map = GameObject.FindGameObjectWithTag("Map");
        GameObject tst = Map.transform.GetChild(nowMapIndex).gameObject;
        tst.SetActive(true);
    }
}
