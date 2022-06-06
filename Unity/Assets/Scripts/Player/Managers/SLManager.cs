using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;

class PlayerData
{
    public Vector3 spawnPoint;
    public string lastSpawnMapName;
    public int nowPlayingBGMIndex;

    public PlayerData(Vector3 spawnPoint, string lastSpawnMapName, int nowPlayingBGMIndex)
    {
        this.spawnPoint = spawnPoint;
        this.lastSpawnMapName = lastSpawnMapName;
        this.nowPlayingBGMIndex = nowPlayingBGMIndex;
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

    public void Save()
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        
        _playerData = new PlayerData(CheckPointManager.instance.spawnPoint,
                                    CheckPointManager.instance.lastSpawnMapName,
                                    SoundManager.instance.nowPlayingBGMIndex);
        string jdata = JsonConvert.SerializeObject(_playerData, settings);
        File.WriteAllText(Application.dataPath + "/gameData.json", jdata);
    }

    public void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/gameData.json");
        _playerData = JsonConvert.DeserializeObject<PlayerData>(jdata);
        CheckPointManager.instance.spawnPoint = _playerData.spawnPoint;
        CheckPointManager.instance.lastSpawnMapName = _playerData.lastSpawnMapName;
        CheckPointManager.instance.nowMapName = _playerData.lastSpawnMapName;
        SoundManager.instance.nowPlayingBGMIndex = _playerData.nowPlayingBGMIndex;
    }
  
}
