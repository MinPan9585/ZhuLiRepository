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
    private bool isDashing;
    private float dashingPower = -20f;
    private float dashingTime = 0.1f;
    private float noDashTime = 0.1f;



    private enum MovementState { idle, running, jumping, falling, dashing }

       private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

       private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.W) && IsGrounded())
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

        if (Input.GetKeyDown(KeyCode.S)&&canDash)
        {
            StartCoroutine(Dash());
        }

        UpdateAnimationUpdate();
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float dashingGravity = rb.gravityScale;
        rb.gravityScale = 0f;

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
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .001f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.001f&& rb.velocity.y > -100f)
        {
            state = MovementState.falling;
        }
        //else
        //{ 
            //state = MovementState.dashing;
        //}

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }



   
    //transform.localScale.y * 
}
