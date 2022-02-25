using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float fieldOfImpact;
    public float ExplodePower;

    public float ThrowPower;
    public float TimeBeforeExplode = 3;
    public Vector3 LaunchOffset;
    
    public LayerMask LayerToHit;

    private float direction;

    private bool isThrow;

    // Update is called once per frame

    private void Awake(){
        var direction = -gameObject.transform.right + Vector3.up;  
        if(gameObject.transform.localScale.x >= 0.01f)
            direction = gameObject.transform.right + Vector3.up;
        
        
        GetComponent<Rigidbody2D>().AddForce(direction * ThrowPower, ForceMode2D.Impulse);

        transform.Translate(gameObject.transform.position);
        
        Invoke("explode",TimeBeforeExplode);
        Debug.Log("Throw bomb");
    }
    void Update()
    {
         Debug.Log("bomb update: "+ gameObject.transform.position);
    }

    void explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);
        foreach(Collider2D obj in objects){
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * ExplodePower);
            //do damage

        }
        Debug.Log("Bomb explode");
        gameObject.SetActive(false);
    }



    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }

     public void SetDirection(float _direction, Transform _playerPosition){
        direction = _direction;
        gameObject.SetActive(true);

        transform.localScale = new Vector2(_playerPosition.transform.localScale.x, _playerPosition.transform.localScale.y);
    }
}
