using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossDie : MonoBehaviour
{
    [SerializeField] private Stage3Boss _instance;

    private void Start()
    {
        _instance = FindObjectOfType<Stage3Boss>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.CompareTag("Player"))
        {
            SoundManager.instance.PlayBossSFX(11);
            _instance.StartDeadCo();
            PlayerController.instance.CanMove = false;
        }
    }
}
