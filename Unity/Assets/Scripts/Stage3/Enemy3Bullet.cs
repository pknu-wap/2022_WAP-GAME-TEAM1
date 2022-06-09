using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(Enemy3BulletCo());
    }

    IEnumerator Enemy3BulletCo()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            transform.Translate(new Vector2(-speed * transform.localScale.x * Time.deltaTime, 0f));
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            GameManager.instance.isDead = true;
        anim.SetTrigger("Destroy");
        Destroy(gameObject, 0.3f);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
