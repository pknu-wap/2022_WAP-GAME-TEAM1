using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceParticle : MonoBehaviour
{
    [SerializeField] float bounceForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                PlayerController.instance.Bounce(bounceForce);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                PlayerController.instance.Bounce(bounceForce);
        }
    }
}
