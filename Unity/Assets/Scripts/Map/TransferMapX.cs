using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMapX : MonoBehaviour
{

    [SerializeField] Vector3 beforeCameraPosition;
    [SerializeField] Vector3 afterCameraPosition;
    [SerializeField] GameObject beforeMap;
    [SerializeField] GameObject afterMap;
    [SerializeField] bool isRight;
    [SerializeField] int beforeMapIndex;
    [SerializeField] int afterMapIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CameraManager.instance.transform.position == beforeCameraPosition)
            {
                CameraManager.instance.transform.position = afterCameraPosition;
                afterMap.SetActive(true);
                beforeMap.SetActive(false);
            }
            else
            {
                CameraManager.instance.transform.position = beforeCameraPosition;
                beforeMap.SetActive(true);
                afterMap.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isRight)
            {
                if (transform.position.x > PlayerController.instance.transform.position.x)
                {
                    CameraManager.instance.transform.position = beforeCameraPosition;
                    beforeMap.SetActive(true);
                    afterMap.SetActive(false);
                }
                else if (transform.position.x < PlayerController.instance.transform.position.x)
                {
                    CameraManager.instance.transform.position = afterCameraPosition;
                    afterMap.SetActive(true);
                    beforeMap.SetActive(false);
                    
                }
            }
            else
            {
                if (transform.position.x < PlayerController.instance.transform.position.x)
                {
                    CameraManager.instance.transform.position = beforeCameraPosition;
                    beforeMap.SetActive(true);
                    afterMap.SetActive(false);
                }
                else if (transform.position.x > PlayerController.instance.transform.position.x)
                {
                    CameraManager.instance.transform.position = afterCameraPosition;               
                    afterMap.SetActive(true);
                    beforeMap.SetActive(false);
                }
            }
        }

    }
}
