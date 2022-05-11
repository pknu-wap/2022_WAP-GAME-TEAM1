using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] float fallSec = 0.5f, destroySec = 2f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OncollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name.Equals("Player")){
            Invoke("FallBlock", fallSec);
            Destroy(gameObject, destroySec);
        }
    }
    void FallBlock(){
        rb.isKinematic = false;
    }
}
