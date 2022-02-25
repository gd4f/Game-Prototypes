using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private GameObject hook;
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
    }

    private void Attack(){
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //pool fireball     
        fireballs.transform.GetChild(FindFireball()).transform.position = firepoint.position;
        fireballs.transform.GetChild(FindFireball()).GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball(){
        Debug.Log("child  count: " +fireballs.transform.childCount);
        for (int i = 0; i < fireballs.transform.childCount; i++){

            if (!fireballs.transform.GetChild(i).gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void ThrowHook(){
        if(Input.GetKeyDown(KeyCode.E)){
            hook.SetActive(true);
            isThrowHook = true;
        }
        if(isThrowHook == true)
            hook.transform.Translate(15*Time.deltaTime * Mathf.Sign(transform.localScale.x), 0, 0);
    }
}
