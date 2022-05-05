using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredSurprisedObject : Trap
{
    public Sprite spikeSprite;
    private SpriteRenderer theSR;

    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            theSR.sprite = spikeSprite;
    }
}
