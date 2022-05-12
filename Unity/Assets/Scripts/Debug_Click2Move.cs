using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Click2Move : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Input.mousePosition;
            player.transform.position = mousePos;
        }
    }
}
