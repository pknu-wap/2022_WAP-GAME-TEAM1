using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {   
        if (GameManager.instance.isMapChanging)
        {
            player = FindObjectOfType<PlayerController>();
            player.transform.position = transform.position;
            GameManager.instance.isMapChanging = false;
        }
    }
}
