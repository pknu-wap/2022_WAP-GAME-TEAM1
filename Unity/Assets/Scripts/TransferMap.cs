using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMap;
    //디버그용
    string _transferMap = "Map1";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.isMapChanging = true;
            SceneManager.LoadScene(transferMap);
            CheckPointManager.instance.nowMapName = transferMap;
        }
    }

    //디버그용 맵 이동
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _transferMap = "Map1";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _transferMap = "Map2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _transferMap = "Map3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _transferMap = "Map4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _transferMap = "Boss1";
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.instance.isMapChanging = true;
            SceneManager.LoadScene(_transferMap);
            CheckPointManager.instance.nowMapName = _transferMap;
        }
    }
}
