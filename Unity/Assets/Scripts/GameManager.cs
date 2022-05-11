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

                // ������ üũ ����Ʈ�� �ٸ� �ʿ� ���� ���
                if (CheckPointManager.instance.lastSpawnMapName != CheckPointManager.instance.nowMapName)
                {
                    // �� �̵��� ���� ���� �ʱ�ȭ
                    CheckPointManager.instance.nowMapName = CheckPointManager.instance.lastSpawnMapName;
                    CheckPointManager.instance.nextMapName = CheckPointManager.instance.nowMapName;

                    SceneManager.LoadScene(CheckPointManager.instance.lastSpawnMapName);
                }
                
                // ������ üũ����Ʈ�� ���� ��
                else
                {
                    SceneManager.LoadScene(CheckPointManager.instance.nowMapName);
                }
            }
        }

        // ��������
        if (isSpawning)
        {
            isSpawning = false;
            PlayerController.instance.transform.position = CheckPointManager.instance.spawnPoint;
            PlayerController.instance.gameObject.SetActive(true);
            SoundManager.instance.PlayBGM(SoundManager.instance.nowPlayingBGMIndex);
        }
    }

   

}
