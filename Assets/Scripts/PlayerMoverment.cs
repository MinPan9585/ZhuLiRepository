using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    //add grounded effects
    public GameObject isGrouded;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 2f;

    //Dash
    private bool canDash = true;
    public bool isDashing;
    private float dashingPower = -15f;
    private float dashingTime = 0.1f;
    private float noDashTime = 0.1f;

    public bool isDead;

    private enum MovementState { idle, running, jumping, falling, dashing }

    //bullet time
    //[SerializeField][Range(0, 1)] float time;

    //ghost
    public GameObject dashObj;

    private void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        transform.position = SaveManager.Instance.lastPosition;

        dashObj.SetActive(true);
    }

    private void Update()
    {
        if (!isDead)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
            {
                Instantiate(isGrouded, transform.position, Quaternion.identity);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            /* if (Input.GetButtonDown("Jump") && IsGrounded())
             {
                 Instantiate(isGrouded, transform.position, Quaternion.identity);
                 rb.velocity = new Vector2(rb.velocity.x, jumpForce);
             }*/



            //Dash ++

            if (isDashing)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.S) && canDash)
            {
                
                StartCoroutine(Dash());
                //print("slow");
                //BulletTime.bt.time = 0;
                //Mathf.Lerp(BulletTime.bt.time, Time.timeScale, 1f);
                
                
                //TimeController.tc.bulletTimeScale = 0.1f;
                //Time.fixedDeltaTime = TimeController.tc.defaultFixedDeltaTime * Time.timeScale;
            }

            UpdateAnimationUpdate();
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;



        float dashingGravity = rb.gravityScale;
        rb.gravityScale = 0f;

  
        //rb.velocity = new Vector2(0f, 0f);
        //yield return new WaitForSeconds(0.1f);
        //rb.velocity = new Vector2(0f, 0.00001f);
        anim.SetTrigger("Player_Dashing");
        //yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0f, dashingPower);

        yield return new WaitForSeconds(dashingTime);

        //anim.SetTrigger("Player_Dashing");

        rb.gravityScale = dashingGravity;

        isDashing = false;
        yield return new WaitForSeconds(noDashTime);
        canDash = true;
    }


    private void UpdateAnimationUpdate()
    {
        //print(rb.velocity.y);
        MovementState state;

        if (dirX > 0f && rb.velocity.y == 0)
        {
            //print("running");
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f && rb.velocity.y == 0)
        {
            //print("running");
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if(dirX == 0f && rb.velocity.y == 0)
        {
            //print("idle");
            state = MovementState.idle;
        }

        else if (rb.velocity.y > .001f)
        {
            //print("jjjjj");
            state = MovementState.jumping;
        }
        else //if (rb.velocity.y < -.001f)
        {
            //print("fff");
            state = MovementState.falling;
        }




 /*       else if(rb.velocity.y <= -1f)
        {
            print("ddddddddddddd");
            state = MovementState.dashing;
        }
        else
        {
            state = MovementState.idle;
        }*/

        /*if (dirX > 0f && rb.velocity.y == 0)
        {
            print("running");
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f && rb.velocity.y == 0)
        {
            print("running");
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (dirX == 0f && rb.velocity.y == 0)
        {
            print("idle");
            state = MovementState.idle;
        }

        else if (rb.velocity.y > .001f)
        {
            print("jjjjj");
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.001f && rb.velocity.y > -1f)
        {
            print("fff");
            state = MovementState.falling;
        }
        else if (rb.velocity.y <= -1f)
        {
            print("ddddddddddddd");
            state = MovementState.dashing;
        }
        else
        {
            state = MovementState.idle;
        }*/

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }



   
    //transform.localScale.y * 
}




/*public float time;
private void Awake()
{
    bt = this;
}

private void Update()
{
    Time.timeScale = time;


}
*/