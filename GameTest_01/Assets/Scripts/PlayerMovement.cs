
using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float glidePower;
    [SerializeField] private float blinkPower;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystem _particleSystem_postblink;

    private Rigidbody2D body;

    private Animator anim;
    private BoxCollider2D boxcollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private bool glideIsOn;
    

    private void Awake(){
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        horizontalInput = Input.GetAxis("Horizontal");
        //body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);
    
        //Flip player when moving sideways
        if (horizontalInput > 0.01f){
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f){
            transform.localScale = new Vector2(-1, 1);
           
        }

         Blink(); 

       

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall Jump Logic
        if(wallJumpCooldown < 0.2f){
            //jump
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                
            if (onWall() && !isGrounded()){
                body.gravityScale = 0;  
                body.velocity = Vector2.zero;
            }else{

                Gliding();
            }
            
            if(Input.GetKey(KeyCode.Space))
                Jump();
        }else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Gliding(){
        //gliding
        if(Input.GetKey(KeyCode.LeftShift)){
            body.gravityScale = glidePower;
        }else   
            body.gravityScale = 5;
            
    }
    private void Jump(){

        
        if(isGrounded()){
            body.velocity = new Vector2(body.velocity.x + 10, jumpPower); 
            anim.SetTrigger("jump");
        }else if(onWall() && !isGrounded()){
            
        }
    }

    private void Blink(){
        if(Input.GetKeyDown(KeyCode.Q)){
            _particleSystem.transform.position = body.position;
            _particleSystem.Play();
           

            if (transform.localScale.x >= 0.01f){
                body.transform.position = new Vector2(body.position.x + blinkPower , body.position.y);
                _particleSystem_postblink.transform.position = new Vector2(body.position.x + blinkPower , body.position.y);
                _particleSystem_postblink.Play();
            }
            else{
                body.transform.position = new Vector2(body.position.x + (blinkPower * -1), body.position.y);
                _particleSystem_postblink.transform.position = new Vector2(body.position.x + (blinkPower * -1), body.position.y);
                _particleSystem_postblink.Play();
            }

            
        }

    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return true;//horizontalInput == 0 && isGrounded() && !onWall();
    }
}
    