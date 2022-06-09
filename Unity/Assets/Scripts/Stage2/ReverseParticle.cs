using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseParticle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                PlayerController.instance.Reverse();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                PlayerController.instance.Reverse();
            }
        }
        
    }
}
