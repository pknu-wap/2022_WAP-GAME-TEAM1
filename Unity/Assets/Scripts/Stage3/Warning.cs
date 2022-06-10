using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] float speed;
    private bool _switch;

    void OnEnable()
    {
        SoundManager.instance.PlayBossSFX(8);
        StartCoroutine(WarningCo());
    }

    IEnumerator WarningCo()
    {
        while (true)
        {
            transform.Translate(new Vector2(0f, speed * Time.deltaTime));

            if (transform.position.y >= 176f && !_switch)
            {
                yield return new WaitForSeconds(3f);
                _switch = true;
            }
            else if (transform.position.y >= 447f)
            {
                break;
            }

            yield return null;
        }
        transform.position = new Vector2(267f, -57f);
        gameObject.SetActive(false);
    }
}
