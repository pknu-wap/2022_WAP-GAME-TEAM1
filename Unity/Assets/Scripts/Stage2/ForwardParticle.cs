using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardParticle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                PlayerController.instance.Forward();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                PlayerController.instance.Forward();
            }
        }
    }

}
