using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap2 : MonoBehaviour
{
    [SerializeField] GameObject setInActiveMap;
    [SerializeField] GameObject setActiveMap;
    [SerializeField] GameObject _camera;
    [SerializeField] float cameraMoveAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        setInActiveMap.SetActive(false);
        _camera.transform.Translate(new Vector2(cameraMoveAmount, 0f));
        setActiveMap.SetActive(true);
    }
}
