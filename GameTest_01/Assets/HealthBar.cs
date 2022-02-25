using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float damage;
    [SerializeField] private float fireballDamage = 5f;
    [SerializeField] private GameObject Health;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystem _particleSystem_crit;

    [SerializeField] private float CriticalChance = 20f;



    private float fixnumberhealth;
    private float hit = 1;
    // Start is called before the first frame update
    
    void Awake(){
        fixnumberhealth = Health.transform.localPosition.x;//1.49
        Debug.Log("damage: " + damage + " hit: " + hit + " fix: " + fixnumberhealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Health.transform.position = new Vector2(1,1);
        if(Health.transform.localPosition.x <= 0f){
             _particleSystem.transform.position = gameObject.transform.position;
            _particleSystem.Play();
            Destroy(gameObject,0.24f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Projectile"){
            
            
            int crit = Random.Range(1,6);
            float s = 0 ;
            if(crit == 1 ){
                s = CriticalChance;
                _particleSystem_crit.Play();
            }
            damage = (fixnumberhealth * (((fireballDamage + (((s/100f) * fireballDamage)))/100)));
            // if(HealthNumber > 0)
                Health.transform.position = new Vector2(Health.transform.position.x - damage, Health.transform.position.y);
            Debug.Log("damage: " + damage + " hit: " + hit + " fix: " + fixnumberhealth);
            hit += 1;
        }
    }
}
