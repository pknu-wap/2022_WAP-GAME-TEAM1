using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    void OnMouseDown()
    {
        GameManager.instance.loadGame();
    }
}
