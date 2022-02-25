using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooked : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy"){
            collision.transform.position = new Vector2(gameObject.transform.parent.position.x + 1, gameObject.transform.parent.position.y);
            gameObject.SetActive(false);
        }
    }
}
