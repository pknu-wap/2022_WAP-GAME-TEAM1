using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDead;
    public bool isMapChanging;

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

    //리스폰
    public void Spawning()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.nowPlayingBGMIndex);
        PlayerController.instance.transform.position = CheckPointManager.instance.spawnPoint;
        PlayerController.instance.gameObject.SetActive(true);
    }
   
    public void StartDeadCo()
    {
        StartCoroutine(DeadCo());
    }

    public IEnumerator DeadCo()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isDead = false;

                // 마지막 체크 포인트가 다른 맵에 있을 경우
                if (CheckPointManager.instance.lastSpawnMapName != CheckPointManager.instance.nowMapName)
                {
                    // 맵 이동을 위한 변수 초기화
                    CheckPointManager.instance.nowMapName = CheckPointManager.instance.lastSpawnMapName;

                    SceneManager.LoadScene(CheckPointManager.instance.lastSpawnMapName);
                }

                // 마지막 체크포인트가 같은 맵
                else
                {
                    SceneManager.LoadScene(CheckPointManager.instance.nowMapName);
                }
                Spawning();
                break;
            }

            yield return null;
        }
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
