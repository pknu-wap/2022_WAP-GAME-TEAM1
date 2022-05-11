using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDead;
    private bool isSpawning;

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

    private void Update()
    {
        if (isDead == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isSpawning = true;
                isDead = false;

                // 마지막 체크 포인트가 다른 맵에 있을 경우
                if (CheckPointManager.instance.lastSpawnMapName != CheckPointManager.instance.nowMapName)
                {
                    // 맵 이동을 위한 변수 초기화
                    CheckPointManager.instance.nowMapName = CheckPointManager.instance.lastSpawnMapName;
                    CheckPointManager.instance.nextMapName = CheckPointManager.instance.nowMapName;

                    SceneManager.LoadScene(CheckPointManager.instance.lastSpawnMapName);
                }
                
                // 마지막 체크포인트가 같은 맵
                else
                {
                    SceneManager.LoadScene(CheckPointManager.instance.nowMapName);
                }
            }
        }

        // 리스폰중
        if (isSpawning)
        {
            isSpawning = false;
            PlayerController.instance.transform.position = CheckPointManager.instance.spawnPoint;
            PlayerController.instance.gameObject.SetActive(true);
            SoundManager.instance.PlayBGM(SoundManager.instance.nowPlayingBGMIndex);
        }
    }

   

}
