using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource[] soundEffects;

    [SerializeField]
    private AudioSource[] bgm;

    [SerializeField]
    private AudioSource gameOverMusic;

    public int nowPlayingBGMIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void PlayBGM(int soundToPlay)
    {
        gameOverMusic.Stop();
        bgm[nowPlayingBGMIndex].Stop();
        bgm[soundToPlay].Play();
        nowPlayingBGMIndex = soundToPlay;
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].Play();
    }

    public void PlayGameOver()
    {
        bgm[nowPlayingBGMIndex].Stop();
        gameOverMusic.Play();
    }
}
