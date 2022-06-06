using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDead;
    public bool isMapChanging = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isDead = false;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            {
                reGame();
            }
    }
    public void Spawning()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.nowPlayingBGMIndex);
        PlayerController.instance.transform.position = CheckPointManager.instance.spawnPoint;
        PlayerController.instance.gameObject.SetActive(true);
    }
   
    public void StartDeadCo()
    {
        // StartCoroutine(DeadCo());
    }

    // public IEnumerator DeadCo()
    // {
    //     while (true)
    //     {
    //         if (Input.GetKeyDown(KeyCode.R))
    //         {
    //             reGame();
    //         }
    //         yield return null;
    //     }
    // }

    public void reGame()
    {
        PlayerController.instance.blockSpeed = new Vector2(0,0);
        isDead = false;
        if (CheckPointManager.instance.lastSpawnMapName != CheckPointManager.instance.nowMapName)
        {
            CheckPointManager.instance.nowMapName = CheckPointManager.instance.lastSpawnMapName;
            SceneManager.LoadScene(CheckPointManager.instance.lastSpawnMapName);
        }
        else
        {
            SceneManager.LoadScene(CheckPointManager.instance.nowMapName);
        }
        Spawning();
    }

    public void newGame()
    {
        SceneManager.LoadScene("Map1");
        Spawning();
    }

    public void loadGame()
    {
        SLManager.instance.Load();
        SceneManager.LoadScene(CheckPointManager.instance.lastSpawnMapName);
        Spawning();
    }
}
