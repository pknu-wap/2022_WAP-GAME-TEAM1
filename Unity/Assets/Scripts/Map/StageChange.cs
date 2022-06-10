using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    [SerializeField] int PlayBgmIndex;
    [SerializeField] string nextStage;
    [SerializeField] Vector3 player_position;

    void Start()
    {
        SoundManager.instance.StopBGM(SoundManager.instance.nowPlayingBGMIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = player_position;
            GameManager.instance.isMapChanging = true;
            SoundManager.instance.PlayBGM(PlayBgmIndex);
            SoundManager.instance.nowPlayingBGMIndex = PlayBgmIndex;
            CheckPointManager.instance.nowMapName = nextStage;
            SceneManager.LoadScene(nextStage);   
        }
    }
}

