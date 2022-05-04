using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDead;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDead = false;
    }

    
    void Update()
    {
        
    }
}
