using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;

class PlayerData
{
    public Vector3 spawnPoint;
    public Vector3 lastCameraPosition;
    public string lastSpawnMapName;
    public int lastPlayingBGMIndex;
    public int nowMapIndex;

    public PlayerData(Vector3 spawnPoint, Vector3 lastCameraPosition, string lastSpawnMapName, int lastPlayingBGMIndex,int nowMapIndex)
    {
        this.spawnPoint = spawnPoint;
        this.lastCameraPosition = lastCameraPosition;
        this.lastSpawnMapName = lastSpawnMapName;
        this.lastPlayingBGMIndex = lastPlayingBGMIndex;
        this.nowMapIndex = nowMapIndex;
    }
}

public class SLManager : MonoBehaviour
{
    public static SLManager instance;
    PlayerData _playerData;

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

    //세이브
    public void Save()
    {
        _playerData = new PlayerData(CheckPointManager.instance.spawnPoint, CheckPointManager.instance.lastCameraPosition, CheckPointManager.instance.lastSpawnMapName, SoundManager.instance.nowPlayingBGMIndex
            , CheckPointManager.instance.nowMapIndex);
        string jdata = JsonConvert.SerializeObject(_playerData);
        File.WriteAllText(Application.dataPath + "/gameData.json", jdata);
    }

    // 로드 
    public void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/gameData.json");
        _playerData = JsonConvert.DeserializeObject<PlayerData>(jdata);
        CheckPointManager.instance.spawnPoint = _playerData.spawnPoint;
        CheckPointManager.instance.lastCameraPosition = _playerData.lastCameraPosition;
        CheckPointManager.instance.lastSpawnMapName = _playerData.lastSpawnMapName;
        CheckPointManager.instance.nowMapName = _playerData.lastSpawnMapName;
        SoundManager.instance.lastPlayingBGMIndex = _playerData.lastPlayingBGMIndex;
        CheckPointManager.instance.nowMapIndex = _playerData.nowMapIndex;
    }
  
}
