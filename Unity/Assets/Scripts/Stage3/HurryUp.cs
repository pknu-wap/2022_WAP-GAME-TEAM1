using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurryUp : MonoBehaviour
{
    [SerializeField] float speed;
    private bool _switch;

    void OnEnable()
    {
        Time.timeScale = 0f;
        SoundManager.instance.PlayBossSFX(8);
        StartCoroutine(HurryUpCo());
    }


    IEnumerator HurryUpCo()
    {
        while (true)
        {
            transform.Translate(new Vector2(0f, speed * Time.unscaledDeltaTime));

            if (transform.position.y >= 146.5f && !_switch)
            {
                yield return new WaitForSecondsRealtime(1.5f);
                _switch = true;
            }
            else if (transform.position.y >= 395f)
            {
                Time.timeScale = 1f;
                break;
            }

            yield return null;
        }
        SoundManager.instance.PlayBGM(8);
        gameObject.SetActive(false);
    }


    
}
