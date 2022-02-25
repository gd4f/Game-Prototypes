using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private GameObject hook;
    [SerializeField] private GameObject Grenade;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private bool isThrowHook;

    private void Awake(){
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        fireballs = Instantiate(fireballs, new Vector2(0f,0f), Quaternion.identity);
    }

    private void Update(){
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }
        cooldownTimer += Time.deltaTime;
        ThrowHook();
        ThrowGrenade();
    }

    private void Attack(){
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //pool fireball     
        fireballs.transform.GetChild(FindFireball()).transform.position = firepoint.position;
        fireballs.transform.GetChild(FindFireball()).GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball(){

        for (int i = 0; i < fireballs.transform.childCount; i++){

            if (!fireballs.transform.GetChild(i).gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void ThrowHook(){
        if(Input.GetKeyDown(KeyCode.E)){
            hook.transform.position = firepoint.position;
            Debug.Log("localscale: " + transform.localScale.x);
            hook.transform.GetComponent<Hooked>().SetDirection(Mathf.Sign(transform.localScale.x), transform);
        }
    }

    private void ThrowGrenade(){
        if(Input.GetKeyDown(KeyCode.G)){
            Grenade.transform.position = firepoint.position;
            Grenade.transform.GetComponent<BombScript>().SetDirection(Mathf.Sign(transform.localScale.x), transform);
        }
    }
}
