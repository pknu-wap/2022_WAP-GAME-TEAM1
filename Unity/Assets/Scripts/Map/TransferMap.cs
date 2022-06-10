using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMap;
    public Vector3 player_Position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = player_Position;
            GameManager.instance.isMapChanging = true;
            CheckPointManager.instance.nowMapName = transferMap;
            SceneManager.LoadScene(transferMap);
        }
            
    }

}
