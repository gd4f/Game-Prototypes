using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooked : MonoBehaviour
{
    private bool hit = false;
    [SerializeField] private float speed = 20;
    private float direction;
    private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy"){
            hit = true;
            collision.transform.position = new Vector2(playerPosition.position.x + 1, playerPosition.position.y);
            gameObject.SetActive(false);
        }
    }

    public void SetDirection(float _direction, Transform _playerPosition){
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        playerPosition = _playerPosition;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }
}
