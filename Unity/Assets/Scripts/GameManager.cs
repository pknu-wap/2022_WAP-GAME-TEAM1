using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIGameOver;

    public bool isDead;
    public bool showDead;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDead = false;
        showDead = false;
    }

    
    void Update()
    {
        if (isDead && !showDead) //죽었을 때 한번만 작동
        {
            showDead = true;
            GameObject objUIGameOver = Instantiate(UIGameOver);
        }
    }
}
