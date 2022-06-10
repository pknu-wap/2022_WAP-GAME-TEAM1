using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    [SerializeField] GameObject map1;
    void Start()
    {
        if (GameManager.instance.isMapChanging)
        {
            if (map1 != null)
                map1.SetActive(true);
            PlayerController.instance.gameObject.SetActive(true);
            GameManager.instance.isMapChanging = false;
        }
    }
}
